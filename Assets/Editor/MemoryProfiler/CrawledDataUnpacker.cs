using System.Collections.Generic;
using System.Linq;
using UnityEditor.MemoryProfiler;
using System;
using UnityEngine;

namespace MemoryProfilerWindow
{
    class CrawlDataUnpacker
    {
        public static CrawledMemorySnapshot Unpack(PackedCrawlerData packedCrawlerData)
        {
            var packedSnapshot = packedCrawlerData.packedMemorySnapshot;

            var result = new CrawledMemorySnapshot
            {
                nativeObjects = packedSnapshot.nativeObjects.Select(packedNativeUnityEngineObject => UnpackNativeUnityEngineObject(packedSnapshot, packedNativeUnityEngineObject)).ToArray(),
                managedObjects = packedCrawlerData.managedObjects.Select(pm => UnpackManagedObject(packedSnapshot, pm)).ToArray(),
                gcHandles = packedSnapshot.gcHandles.Select(pgc => UnpackGCHandle(packedSnapshot)).ToArray(),
                staticFields = packedSnapshot.typeDescriptions.Where(t => t.staticFieldBytes != null & t.staticFieldBytes.Length > 0).Select(t => UnpackStaticFields(t)).ToArray(),
                typeDescriptions = packedSnapshot.typeDescriptions,
                managedHeap = packedSnapshot.managedHeapSections,
                nativeTypes = packedSnapshot.nativeTypes,
                virtualMachineInformation = packedSnapshot.virtualMachineInformation
            };

            var combined = new ThingInMemory[0].Concat(result.gcHandles).Concat(result.nativeObjects).Concat(result.staticFields).Concat(result.managedObjects).ToArray();
            result.allObjects = combined;

            var referencesLists = MakeTempLists(combined);
            var referencedByLists = MakeTempLists(combined);

            foreach (var connection in packedCrawlerData.connections)
            {
                referencesLists[connection.@from].Add(combined[connection.to]);
                referencedByLists[connection.to].Add(combined[connection.@from]);
            }

            for (var i = 0; i != combined.Length; i++)
            {
                combined[i].references = referencesLists[i].ToArray();
                combined[i].referencedBy = referencedByLists[i].ToArray();
            }

            return result;
        }

        static List<ThingInMemory>[] MakeTempLists(ThingInMemory[] combined)
        {
            var referencesLists = new List<ThingInMemory>[combined.Length];
            for (int i = 0; i != referencesLists.Length; i++)
                referencesLists[i] = new List<ThingInMemory>(4);
            return referencesLists;
        }

        static StaticFields UnpackStaticFields(TypeDescription typeDescription)
        {
            return new StaticFields()
                   {
                       typeDescription = typeDescription,
                       caption = "static fields of " + typeDescription.name,
                       size = typeDescription.staticFieldBytes.Length
                   };
        }

        static GCHandle UnpackGCHandle(PackedMemorySnapshot packedSnapshot)
        {
            return new GCHandle() { size = packedSnapshot.virtualMachineInformation.pointerSize, caption = "gchandle" };
        }

        static ManagedObject UnpackManagedObject(PackedMemorySnapshot packedSnapshot, PackedManagedObject pm)
        {
            var typeDescription = packedSnapshot.typeDescriptions[pm.typeIndex];
            return new ManagedObject() { address = pm.address, size = pm.size, typeDescription = typeDescription, caption = typeDescription.name };
        }

        static NativeUnityEngineObject UnpackNativeUnityEngineObject(PackedMemorySnapshot packedSnapshot, PackedNativeUnityEngineObject packedNativeUnityEngineObject)
        {
            var className = packedSnapshot.nativeTypes[packedNativeUnityEngineObject.nativeTypeArrayIndex].name;

            return new NativeUnityEngineObject()
                   {
                       instanceID = packedNativeUnityEngineObject.instanceId,
                       classID = packedNativeUnityEngineObject.nativeTypeArrayIndex,
                       className = className,
                       name = packedNativeUnityEngineObject.name,
                       caption = packedNativeUnityEngineObject.name + "(" + className + ")",
                       size = packedNativeUnityEngineObject.size,
                       isPersistent = packedNativeUnityEngineObject.isPersistent,
                       isDontDestroyOnLoad = packedNativeUnityEngineObject.isDontDestroyOnLoad,
                       isManager = packedNativeUnityEngineObject.isManager,
                       hideFlags = packedNativeUnityEngineObject.hideFlags
                   };
        }
    }

    [System.Serializable]
    internal class PackedCrawlerData
    {
        public bool valid;
        public PackedMemorySnapshot packedMemorySnapshot;
        public StartIndices startIndices;
        public PackedManagedObject[] managedObjects;
        public TypeDescription[] typesWithStaticFields;
        public Connection[] connections;

        public PackedCrawlerData(PackedMemorySnapshot packedMemorySnapshot)
        {
            this.packedMemorySnapshot = packedMemorySnapshot;
            typesWithStaticFields = packedMemorySnapshot.typeDescriptions.Where(t => t.staticFieldBytes != null && t.staticFieldBytes.Length > 0).ToArray();
            startIndices = new StartIndices(this.packedMemorySnapshot.gcHandles.Length, this.packedMemorySnapshot.nativeObjects.Length, typesWithStaticFields.Length);
            valid = true;
        }
    }

    [System.Serializable]
    internal class StartIndices
    {
        [SerializeField]
        private int _gcHandleCount;
        [SerializeField]
        private int _nativeObjectCount;
        [SerializeField]
        private int _staticFieldsCount;

        public StartIndices(int gcHandleCount, int nativeObjectCount, int staticFieldsCount)
        {
            _gcHandleCount = gcHandleCount;
            _nativeObjectCount = nativeObjectCount;
            _staticFieldsCount = staticFieldsCount;
        }

        public int OfFirstGCHandle { get { return 0; } }
        public int OfFirstNativeObject { get { return OfFirstGCHandle + _gcHandleCount; } }
        public int OfFirstStaticFields { get { return OfFirstNativeObject + _nativeObjectCount; } }
        public int OfFirstManagedObject { get { return OfFirstStaticFields + _staticFieldsCount; } }
    }
}
