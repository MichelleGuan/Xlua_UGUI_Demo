using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using XLua;

 

public static class XLuaHelper
{
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
        // XLuaHelper
        typeof(XLuaHelper),
        typeof(Component),
        typeof(Array),
        typeof(IList),
        typeof(IDictionary),
        typeof(Activator),
        typeof(Type),
        typeof(BindingFlags),
    };

    [CSharpCallLua]
    public static List<Type> CSharpCallLua1 = new List<Type>()
    {
    };

    // 说明：扩展NGUITools.AddMissingComponent方法
    public static Component AddMissingComponent(this GameObject go, Type cmpType)
    {
        Component comp = go.GetComponent(cmpType);
        if (comp == null)
        {
            comp = go.AddComponent(cmpType);
        }
        return comp;
    }

    // 说明：扩展CreateInstance方法
    public static Array CreateArrayInstance(Type itemType, int itemCount)
    {
        return Array.CreateInstance(itemType, itemCount);
    }
    
    public static IList CreateListInstance(Type itemType)
    {
        return (IList)Activator.CreateInstance(MakeGenericListType(itemType));
    }

    public static IDictionary CreateDictionaryInstance(Type keyType, Type valueType)
    {
        return (IDictionary)Activator.CreateInstance(MakeGenericDictionaryType(keyType, valueType));
    }

    // 说明：创建委托
    // 注意：重载函数的定义顺序很重要：从更具体类型（Type）到不具体类型（object）,xlua生成导出代码和lua侧函数调用匹配时都是从上到下的，如果不具体类型（object）写在上面，则永远也匹配不到更具体类型（Type）的重载函数，很坑爹
    public static Delegate CreateActionDelegate(Type type, string methodName, params Type[] paramTypes)
    {
        return InnerCreateDelegate(MakeGenericActionType, null, type, methodName, paramTypes);
    }

    public static Delegate CreateActionDelegate(object target, string methodName, params Type[] paramTypes)
    {
        return InnerCreateDelegate(MakeGenericActionType, target, null, methodName, paramTypes);
    }
     
    
    delegate Type MakeGenericDelegateType(params Type[] paramTypes);
    static Delegate InnerCreateDelegate(MakeGenericDelegateType del, object target, Type type, string methodName, params Type[] paramTypes)
    {
        if (target != null)
        {
            type = target.GetType();
        }

        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        MethodInfo methodInfo = (paramTypes == null || paramTypes.Length == 0) ? type.GetMethod(methodName, bindingFlags) : type.GetMethod(methodName, bindingFlags, null, paramTypes, null);
        Type delegateType = del(paramTypes);
        return Delegate.CreateDelegate(delegateType, target, methodInfo);
    }

    // 说明：构建List类型
    public static Type MakeGenericListType(Type itemType)
    {
        return typeof(List<>).MakeGenericType(itemType);
    }

    // 说明：构建Dictionary类型
    public static Type MakeGenericDictionaryType(Type keyType, Type valueType)
    {
        return typeof(Dictionary<,>).MakeGenericType(new Type[] { keyType, valueType });
    }

    // 说明：构建Action类型

    public static Type MakeGenericActionType(params Type[] paramTypes)
    {
        if (paramTypes == null || paramTypes.Length == 0)
        {
            return typeof(Action);
        }
        else if (paramTypes.Length == 1)
        {
            return typeof(Action<>).MakeGenericType(paramTypes);
        }
        else if (paramTypes.Length == 2)
        {
            return typeof(Action<,>).MakeGenericType(paramTypes);
        }
        else
        {
            return typeof(Action<,,,>).MakeGenericType(paramTypes);
        }
    }

     
}
