


using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
//  0 BuildAssetBundleOptions.None --构建AssetBundle没有任何特殊的选项
//  1 BuildAssetBundleOptions.UncompressedAssetBundle --不进行数据压缩。如果使用该项，因为没有压缩\解压缩的过程， AssetBundle的发布和加载会很快，但是AssetBundle也会更大，下载变慢
//  2 BuildAssetBundleOptions.CollectDependencies  --包含所有依赖关系
//  4 BuildAssetBundleOptions.CompleteAssets  --强制包括整个资源
//  8 BuildAssetBundleOptions.DisableWriteTypeTree --在AssetBundle中不包含类型信息。发布web平台时，不能使用该项
// 16 BuildAssetBundleOptions.DeterministicAssetBundle --使每个Object具有唯一不变的hash ID，可用于增量式发布AssetBundle
// 32 BuildAssetBundleOptions.ForceRebuildAssetBundle --强制重新Build所有的AssetBundle
// 64 BuildAssetBundleOptions.IgnoreTypeTreeChanges --忽略TypeTree的变化，不能与DisableTypeTree同时使用
//128 BuildAssetBundleOptions.AppendHashToAssetBundleName --附加hash到AssetBundle名称中
//256 BuildAssetBundleOptions.ChunkBasedCompression --Assetbundle的压缩格式为lz4。默认的是lzma格式
public class AssetBunldeManager
{
    private static bool isAssetBunldeManagerDebug = false;
    private static bool isCreateCSDebug = false;

    private static bool isDebug = false;
 
    public static readonly string[] BunldeNames = new string[]{
        "Prefab",
        "SpritePack",
        "Texture",
        "Sound",
        "Music",
        "Data"
    };
  

    private static StringBuilder[] list_sb = new StringBuilder[BunldeNames.Length];
    private static StringBuilder[] list_sbPath = new StringBuilder[BunldeNames.Length];

    private static Dictionary<string,string> sb_allName = new Dictionary<string,string>();
     
    public static string pathURL = FileIO.PathURL + "/AssetBunlde/";
    public static string sourcePath = Application.dataPath + "/Res/";
    public static string resourcePath = Application.dataPath + "/Resources/";
    public static string genURL = Application.dataPath + "/Gen/";

    //[MenuItem("WT/AssetBunlde资源管理/生成索引")]
    //static void CreateAssetBunldesIndex()
    //{
    //    ClearAssetBundlesName();

    //    Pack(isDebug ? resourcePath : sourcePath, false);

    //    string outputPath = Path.Combine(pathURL, GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
    //    if (!Directory.Exists(outputPath))
    //    {
    //        Directory.CreateDirectory(outputPath);
    //    }

    //    //根据BuildSetting里面所激活的平台进行打包  

    //    AssetBundleManifest abm = BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

    //    AssetDatabase.Refresh();

    //    WriteAssetBunldesNameCS("R.cs", abm.GetAllAssetBundles());

    //    //刷新编辑器
    //    AssetDatabase.Refresh();
    //}

    [MenuItem("WT/AssetBunlde资源管理/生成索引并打包-lz4")]
    static void CreateAssetBunldesMainByLz4()
    {
        isFirst = true;
        ClearAssetBundlesName();

        Pack(sourcePath, false);

        string outputPath = Path.Combine(pathURL, GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        //根据BuildSetting里面所激活的平台进行打包  
       
        if (!isDebug)
        {//res 中有资源则打包，res中没有资源则不打包
            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        }
           
        WriteAssetBunldesNameCS("R.cs", sb_allName);
        
        AssetDatabase.Refresh(); //刷新编辑器
    }
     
    private static void WriteAssetBunldesNameCS(string name,Dictionary<string,string> txt)
    {
        string[] allTxt = new string[txt.Values.Count];
        txt.Values.CopyTo(allTxt,0);
         
        WriteAssetBunldesNameCS(name, allTxt);
    }

    /// <summary>
    /// 写入索引到.cs
    /// </summary>
    /// <param name="name"></param>
    /// <param name="txt"></param>
    private static void WriteAssetBunldesNameCS(string name, string[] txt)
    {
        int[] bunldeIndex = new int[BunldeNames.Length];

        StringBuilder sbMain = new StringBuilder();
        sbMain.Append("public class R{\n");

        for (int i = 0; i < list_sb.Length; i++)
        {
            list_sb[i] = new StringBuilder();
            list_sb[i].Append("\n\tpublic class " + BunldeNames[i] + "{\n");

            list_sbPath[i] = new StringBuilder();
            list_sbPath[i].Append("\n\t\tpublic static string[] path = {");
        }
          
        foreach (var itemName in txt)
        {
            string abName = itemName.ToLower();
           
            for (int i = 0; i < BunldeNames.Length; i++)
            {
                if (abName.Contains(BunldeNames[i].ToLower()))//该ab的类型
                {
                    list_sb[i].Append(AbNameToIndex(abName, bunldeIndex[i]));
                    bunldeIndex[i]++;
                    list_sbPath[i].Append("\"");
                    list_sbPath[i].Append(abName);
                    list_sbPath[i].Append("\",");
                    if (bunldeIndex[i] != 0 && bunldeIndex[i] % 20 == 0)
                    {
                        list_sbPath[i].Append("\n");
                    }
                }
            }
        }

        for (int i = 0; i < list_sb.Length; i++)
        {
            list_sbPath[i].Append("\n\t\t};\n\t}");
            sbMain.Append(list_sb[i].ToString());

            sbMain.Append(list_sbPath[i].ToString());
        }
         
        sbMain.Append("\n}");

        //Debug.Log("sbMain:"+ sbMain.ToString());

        FileIO.WriteAllAbName(genURL, name, sbMain.ToString());
    }

    private static string AbNameToIndex(string fileName, int index)
    {
        StringBuilder sb = new StringBuilder();
        string[] abName = fileName.Split('/');
        sb.Append("\t\tpublic const int ");

        if (abName.Length < 4) 
        {
            Debug.LogError("资源文件["+ fileName + "]放置位置错误[姓名首字母/模块名/资源文件类型/文件名]!");
            return null;
        }
        for (int i = 3; i < abName.Length - 1; i++)
        {
            sb.Append(abName[i].ToUpper());
            sb.Append("_");
        }
        sb.Append(abName[abName.Length - 1].ToUpper());
        sb.Append(" = ");
        sb.Append(index);
        sb.Append(";\n");

        return sb.ToString();
    }

    private static string AbNameToPath(string fileName, int index)
    {
        StringBuilder sb = new StringBuilder();
        string[] abName = fileName.Split('/');

        for (int i = 0; i < abName.Length; i++)
        {
            sb.Append(abName[i]);
            sb.Append("\",");
            if (i != 0 && i % 20 == 0)
            {
                sb.Append("\n");
            }
        }
        return sb.ToString();
    }

    private static bool isFirst = true;
    /// <summary>
    /// 如上层目录是图集文件夹<SpritePack>,则下层目录按照文件夹分图集
    /// </summary>
    /// <param name="source"></param>
    /// <param name="isSpritePack">是图集目录</param>
    static void Pack(string source, bool isSpritePack)
    {
        DirectoryInfo folder = new DirectoryInfo(source);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        if (isFirst)
        {
            isDebug = false;
        }
       
        
        if (files.Length == 0 && isFirst)
        {
            if (isFirst)
            {
                isDebug = true;
            }
             
            folder = new DirectoryInfo(resourcePath);
            files = folder.GetFileSystemInfos();
        }
        isFirst = false;

        for (int i = 0; i < files.Length; i++)
        {
            //Debug.Log("files[i]:" + files[i]+ ",isSpritePack:"+ isSpritePack);

            if (files[i] is DirectoryInfo)
            {
                if (isSpritePack)
                {
                    SetSpritePackName(files[i].FullName);
                }
                else
                {
                    Pack(files[i].FullName, files[i].Name.EndsWith("SpritePack"));
                }
            }
            else
            {
                if (!files[i].Name.EndsWith(".meta"))
                {
                    SetAbName(files[i].FullName, string.Empty);
                }
            }
        }
    }

    private static void SetSpritePackName(string source)
    {
        DirectoryInfo folder = new DirectoryInfo(source);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            if (!files[i].Name.EndsWith(".meta"))
            {
                string name = source.Substring(source.IndexOf("/") + 1);

                SetAbName(files[i].FullName, files[i].Name);
            }
        }
    }

    private static void SetAbName(string source, string name)
    {
        if (isAssetBunldeManagerDebug)
            Debug.Log("source:" + source + ",name:" + name);

        string _source = Replace(source);
        string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
        string _assetPath2 = _source.Substring(Application.dataPath.Length + 1);

        if (isAssetBunldeManagerDebug)
            Debug.Log("目标文件:" + _assetPath + ",_assetPath2:" + _assetPath2);
         
        AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath); 
        string assetName = _assetPath2.Substring(_assetPath2.IndexOf("/") + 1);

        if (string.IsNullOrEmpty(name))
        {
            assetName = assetName.Replace(Path.GetExtension(assetName), "");
        }
        else
        {
            assetName = string.IsNullOrEmpty(name) ? assetName : assetName.Replace("/" + name, "");
        }

        if (!sb_allName.ContainsKey(assetName))
        {
            sb_allName.Add(assetName,assetName);
        }
        if(!isDebug)
            assetImporter.assetBundleName = assetName;//设置AssetBundleName  
    }

    private static string Replace(string s)
    {
        return s.Replace("\\", "/");
    }

    //[MenuItem("WT/AssetBunlde资源管理/lzma压缩打包")]
    //static void CreateAssetBunldesMainByLzma()
    //{
    //    Debug.Log("PathURL:" + PathURL);
    //    BuildPipeline.BuildAssetBundles(PathURL, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    //    //刷新编辑器
    //    AssetDatabase.Refresh();
    //}

    //[MenuItem("WT/AssetBunlde资源管理/不压缩打包")]
    //static void CreateAssetBunldesMainByUncompressedAssetBundle()
    //{
    //    Debug.Log("PathURL:" + PathURL);
    //    BuildPipeline.BuildAssetBundles(PathURL, BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
    //    //刷新编辑器
    //    AssetDatabase.Refresh();
    //}

    /// <summary>
    /// 查看所有的Assetbundle名称（设置Assetbundle Name的对象）
    /// </summary>
    [MenuItem("WT/AssetBunlde资源管理/查看所有AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        //获取所有设置的AssetBundle 
        foreach (var name in names) Debug.Log("AssetBundle: " + name);
    }

    /// <summary>  
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包  
    /// 之前说过，只要设置了AssetBundleName的，都会进行打包，不论在什么目录下  
    /// </summary>  
    [MenuItem("WT/AssetBunlde资源管理/清除所有AssetBundle names")]
    static void ClearAssetBundlesName()
    {
        int length = AssetDatabase.GetAllAssetBundleNames().Length;

        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }
        length = AssetDatabase.GetAllAssetBundleNames().Length;

    }

    public static string GetPlatformFolder(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "IOS";

            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "OSX";
            default:
                return null;
        }
    }
}

