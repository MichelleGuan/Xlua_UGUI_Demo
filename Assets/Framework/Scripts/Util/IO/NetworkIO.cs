using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
///  网络io类
/// </summary>
public class NetworkIO
{
     /// <summary>
     /// 读取并保存单个资源
     /// </summary>
     /// <param name="path"></param>
     /// <param name="fileName"></param>
     /// <param name="callBack"></param>
     /// <returns></returns>
    public static IEnumerator HttpGetAndSave(string path, string fileName,Action callBack)
    {
        string savePath = "";
        string saveFileName = "";
        PathAndName(fileName, out savePath, out saveFileName);

        Debug.Log("开始下载:"+ path);
        WWW wwwAsset = new WWW(path);
        //获取加载进度  
        while (!wwwAsset.isDone)
        {
            Debug.Log(string.Format("下载 {0} : {1:N1}%", saveFileName, (wwwAsset.progress * 100)));
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("save");
        FileIO.WriteFile(savePath, saveFileName, wwwAsset.bytes);
        Debug.Log("callback");

        if (callBack != null)
        {
            callBack();
        }
        wwwAsset.Dispose();
        yield return null;
    }

    /// <summary>
    /// www下载list中所有资源并保存到本地
    /// </summary> 
    /// <param name="list_path">所有要下载的文件url</param>
    /// <param name="list_fileName">保存到本地的路径和文件名(ui/button)</param>
    /// <param name="callBack">当所有下载完成并保存到本地后，调用此callBack</param>
    /// <returns></returns>
    public static IEnumerator HttpGetAndSave(List<string> list_path, List<string> list_fileName,Action callBack)
    {
        bool isDebug = false;
        for (int i = 0; i < list_path.Count; i++)
        {
            string path = "";
            string fileName = "";
            PathAndName(list_fileName[i],out path,out fileName);

            if (isDebug)
                Debug.Log("开始下载:" + list_path[i]);

            WWW wwwAsset = new WWW(list_path[i]);
           
            while (!wwwAsset.isDone)
            {
                if (isDebug)
                    Debug.Log(string.Format("下载 {0} : {1:N1}%", fileName, (wwwAsset.progress * 100)));
                yield return new WaitForSeconds(0.1f);
            }
          
            FileIO.WriteFile(path, fileName, wwwAsset.bytes);
            wwwAsset.Dispose();
            if (isDebug)
                Debug.Log("save:"+ path + "->"+ fileName);
        }


        if (callBack != null)
        {
            callBack();
        }
       
        yield return null;
    }

    /// <summary>
    /// 根据带路径的文件名（ui/button）和分配的父存储路径(path)，重新组合出该文件的存储目录和文件名(存储:path/ui 文件名:button)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    private static void PathAndName(string abName,out string path,out string fileName)
    {
        string[] fileNames = abName.Split('/');
        StringBuilder sb = new StringBuilder();

        sb.Append(FileIO.Remote_AssetBunlde_PATH);
        for (int j = 0; j < fileNames.Length - 1; j++)
        {
            sb.Append(fileNames[j]);
            sb.Append("/");
        }
        path = sb.ToString();
        fileName = fileNames[fileNames.Length - 1];
    }

    /// <summary>
    /// 同步http下载，下载ab资源时不可用
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static byte[] HttpGet(string url)
    {
        // 设置参数
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

        
        //发送请求并获取相应回应数据
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        //直到request.GetResponse()程序才开始向目标网页发送Post请求
        Stream responseStream = response.GetResponseStream();

        byte[] data = new byte[response.ContentLength];
         
        responseStream.Read(data, 0, data.Length);
         
        responseStream.Close();
        return data;
    }

    public static sbyte[] HttpGetToSbyte(string url)
    {
        // 设置参数
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //发送请求并获取相应回应数据
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        //直到request.GetResponse()程序才开始向目标网页发送Post请求
        Stream responseStream = response.GetResponseStream();

        byte[] data = new byte[response.ContentLength];

        responseStream.Read(data, 0, data.Length);

        sbyte[] signed = Array.ConvertAll(data, b => unchecked((sbyte)b));

        responseStream.Close();
        return signed;
    }
}