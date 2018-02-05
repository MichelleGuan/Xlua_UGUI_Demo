using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public static class HotfixCfg
{
    [Hotfix]
    public static List<Type> by_field = new List<Type>()
    {
      
        typeof(Vector3),

    };

    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
                typeof(FileIO),
                typeof(NetWorkClient),
            };


}