﻿using UnityEngine;
using UnityEditor;
using System.IO;

public class MyPostprocessor : AssetPostprocessor
{
    //使用AssetPostprocessor类定义的函数OnPostprocessAssetbundleNameChanged回调
    //当AssetBundle的名称发生变化时，编辑器会自动执行以下函数，返回变化信息
    public void OnPostprocessAssetbundleNameChanged(string assetPath, string previousAssetBundleName, string newAssetBundleName)
    {
        //Debug.Log("Asset " + assetPath + " has been moved from assetBundle " + previousAssetBundleName + " to assetBundle " + newAssetBundleName);
    }
     
    void OnPostprocessTexture(Texture2D texture)
    {
        string AtlasName = new DirectoryInfo(Path.GetDirectoryName(assetPath)).Name;
        TextureImporter textureImporter = assetImporter as TextureImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spritePackingTag = AtlasName;
        textureImporter.mipmapEnabled = false;
    }
}