using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

/// <summary>
/// 文件io类，已根据资源类型分配路径
/// <para>PathURL = 所有后添加资源父路径</para>
///  <para>xlua = "Hotfix/lua/"</para>
///  <para>AssetBunlde = "AssetBunlde/"</para>
///  <para>Wangtuo 2017/12/29</para>
///  
/// </summary>
public class FileIO
{
    private static bool isABDebug = false;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
   

    /// <summary>
    /// 文件存取路径
    /// </summary>
    public static readonly string PathURL = Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID
     
    public static readonly string PathURL = Application.persistentDataPath + "/";
#elif UNITY_IPHONE
    
    public static readonly string PathURL = Application.persistentDataPath + "/";
#else
    public static readonly string PathURL = string.Empty;
  
#endif

    /// <summary>
    /// xlua热更新路径
    /// </summary>
    public static readonly string HOTFIX_LUA_PATH = PathURL + "Hotfix/lua/";

    /// <summary>
    /// lua pb文件路径
    /// </summary>
    public static readonly string HOTFIX_LUA_PB_PATH = PathURL + "Hotfix/lua/pb/";


    public static readonly string Remote_AssetBunlde_PATH = PathURL + "AssetBunlde/Android/";


    private static Dictionary<string, AssetBundle> map_ab = new Dictionary<string, AssetBundle>();

    private static Dictionary<string, GameObject> map_gameObject = new Dictionary<string, GameObject>();

    /// <summary>
    /// 尝试从缓存中读取ab资源。 如未缓存，则同步加载并缓存
    /// 1.尝试缓存中读取，如没有
    /// 2.尝试读取ab文件，加载AssetBundleManifest
    /// 3.根据AssetBundleManifest加载所有依赖
    /// 4.加载目标AssetBundle
    /// </summary>
    /// <param name="abName"></param>
    /// <returns></returns>
    public static GameObject LoadPrefab(string abName)
    {
        if(isABDebug)
            Debug.Log("abName:"+ abName);
        GameObject gameObject = map_gameObject.GetValue(abName);
        if (gameObject == null)
        {
            AssetBundle obj = map_ab.GetValue(abName);

            if (obj == null)
            {
                List<AssetBundle> list_Dependencies = LoadDependencies(abName);//加载所有依赖
                if (list_Dependencies == null)
                {
                    return null;
                }

                obj = LoadAssetBundle(abName);
                if (obj == null)
                {
                    return null;
                }
                 
                string[] abnames = abName.Split('/');

                gameObject = obj.LoadAsset<GameObject>(abnames[abnames.Length - 1]);
                //obj.Unload(true);

                if (gameObject == null)
                {
                    Debug.LogError("读取[" + abName + "]异常!");
                }
                else
                {
                    map_gameObject.Add(abName, gameObject);
                }
            }
        }

        return gameObject;
    }

    /// <summary>
    /// 加载预制体类型ab资源
    /// </summary>
    /// <param name="abIndex"></param>
    /// <returns></returns>
    public static GameObject LoadPrefab(int abIndex)
    {
        string abName = R.Prefab.path[abIndex];
        GameObject gameObject = LoadPrefab(abName);
        if (gameObject == null)//远程目录为空或者加载失败
        {
            gameObject = Resources.Load(abName) as GameObject;
        }
        return gameObject;
    }

    /// <summary>
    /// 根据名称加载缓存中ab资源和所有依赖的其他ab。如缓存中没有则读取文件并缓存
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static List<AssetBundle> LoadDependencies(string name)
    {
        AssetBundleManifest assetBundleManifest = LoadAssetBundleManifest();
        if (assetBundleManifest == null)
        {
            return null;
        }
 
        List<AssetBundle> list_dependencies = LoadDependencies(assetBundleManifest, name);
        return list_dependencies;
       
    }

    /// <summary>
    /// 获得AssetBundle的所有依赖
    /// </summary>
    /// <param name="abm"></param>
    /// <param name="abName"></param>
    /// <returns></returns>
    public static List<AssetBundle> LoadDependencies(AssetBundleManifest abm, string abName)
    {
        if(isABDebug)
        Debug.Log("LoadDependencies->abName:" + abName);
        List<AssetBundle> list_ab = new List<AssetBundle>();
        foreach (var item in abm.GetAllDependencies(abName))
        {
            if (isABDebug)
                Debug.Log("依赖物品:"+ item);
            if (map_ab.ContainsKey(item))
            {
                list_ab.Add(map_ab[item]);
            }
            else
            {
                if (isABDebug)
                    Debug.Log("依赖不存在，加载:" + item);
                AssetBundle ab = LoadAssetBundle(item);
                if (ab != null)
                {
                    list_ab.Add(ab);
                }
            }
        }
        return list_ab;
    }

    /// <summary>
    /// 读取缓存中AssetBundle文件,如缓存中没有，则读取文件
    /// </summary>
    /// <param name="abName"></param>
    /// <returns></returns>
    public static AssetBundle LoadAssetBundle(string abName)
    {
        AssetBundle ab = null;

        if (map_ab.TryGetValue(abName, out ab))
        {
            return ab;
        }
        else
        {
            string path =  Remote_AssetBunlde_PATH + abName;

            if(isABDebug)
                Debug.Log("LoadAssetBundle:" + path);

            if (FileIO.IsExists(path))//远程文件存在
            {
                ab = AssetBundle.LoadFromFile(path);//优先远程地址
            }

            if (ab == null)//读取失败,尝试读取本地
            {
                return null;
            }
            map_ab.Add(abName, ab);
            return ab;
        }
    }

    private static AssetBundleManifest assetBundleManifest;
    /// <summary>
    /// 加载ab包AssetBundleManifest
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static AssetBundleManifest LoadAssetBundleManifest()
    {
        if (assetBundleManifest == null)
        {
            string path = Remote_AssetBunlde_PATH + "Android";
           
            if (FileIO.IsExists(path))
            {
                AssetBundle manifestBundle = AssetBundle.LoadFromFile(path);
                assetBundleManifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
        }
 
        return assetBundleManifest;
    }

    /// <summary>
    /// 写入data到指定目录，同步方法
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="data"></param>
    public static void WriteFile(string filePath, string fileName, byte[] data)
    {
        if (!IsExists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        FileStream fs = new FileStream(filePath + fileName, FileMode.Create);
        fs.Write(data, 0, data.Length);
         
        fs.Close();
        fs.Dispose();//文件流释放  
    }


    public static void WriteAllAbName(string filePath,string fileName,string text)
    {
        if (!IsExists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        
        File.WriteAllText(filePath+fileName, text);
    }

    /// <summary>
    /// 写入一个lua文件到热更文件夹
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="data"></param>
    public static void WriteLua(string fileName, byte[] data)
    {
        WriteFile(HOTFIX_LUA_PATH, fileName, data);
    }
     
    /// <summary>
    /// 写入一个lua pb文件到热更文件夹
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="data"></param>
    public static void WriteLuaPb(string fileName, byte[] data)
    {
        Debug.Log("写入pb：" + (HOTFIX_LUA_PB_PATH + fileName));
        WriteFile(HOTFIX_LUA_PB_PATH, fileName, data);
    }

    /// <summary>
    /// 获取指定文件md5码
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetFileMd5(string filePath)
    {
        return GetFileMd5(filePath);
    }

    /// <summary>
    /// 加载一个.pb文件，返回byte流。lua pbc专用方法
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static byte[] LoadPB(string fileName)
    {
        FileStream fs = new FileStream(HOTFIX_LUA_PB_PATH + "/" + fileName, FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        return data;
    }

    public static byte[] ReadFile(string path, string fileName)
    {
        FileStream fs = new FileStream(path + fileName, FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);

        sbyte[] signed = Array.ConvertAll(data, b => unchecked((sbyte)b));
        return data;
    }

    public static sbyte[] ReadFileSbyte(string path, string fileName)
    {
        FileStream fs = new FileStream(path + fileName, FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);

        sbyte[] signed = Array.ConvertAll(data, b => unchecked((sbyte)b));
        return signed;
    }

    /// <summary>
    /// 自定义lua加载器
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static byte[] CustomLoaderMethod(ref string fileName)
    {
        string path = HOTFIX_LUA_PATH + fileName;

        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 文件是否存在
    /// </summary>
    /// <param name="file_name"></param>
    /// <returns></returns>
    public static bool IsExists(string filePath)
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// 获取指定文件md5码
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetFileMd5(string path, string fileName)
    {
        try
        {
            //指定文集为根目录下的Test.cs  
            FileStream fs = new FileStream(path + fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }

            return sb.ToString();

        }
        catch (Exception ex)
        {

            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }
}

