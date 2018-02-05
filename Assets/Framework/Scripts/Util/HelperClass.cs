using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 扩展类:
/// 王拓
/// </summary>
internal static class HelperClass
{
    internal static string MapGetString<T>(this Dictionary<int, T> map)
    {
        if (map == null)
        {
            return null;
        }
        List<string> list = new List<string>(map.Select(keyValuePair => "[" + keyValuePair.Key + ":" + keyValuePair.Value + "]"));
        var toString = string.Join(", ", list.ToArray());
        return toString;
    }

    internal static Tvalue GetValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

    public static T LoadObjectByAb<T>(this AssetBundle ab, string abName) where T : UnityEngine.Object
    {
        return ab.LoadAsset<T>(abName);
    }
}


internal static class ListHelperClass
{
    internal static  string GetString<T>(this List<T> list)
    {
        if (list == null)
        {
            return null;
        }
        StringBuilder sb = new StringBuilder();
        foreach (var item in list)
        {
            sb.Append(item);
            sb.Append(",");
        }

        return sb.ToString();
    }
}
