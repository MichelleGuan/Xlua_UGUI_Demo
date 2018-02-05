#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class SystemActivatorWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Activator);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 5, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateComInstanceFrom", _m_CreateComInstanceFrom_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateInstanceFrom", _m_CreateInstanceFrom_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateInstance", _m_CreateInstance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetObject", _m_GetObject_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.Activator does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateComInstanceFrom_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string assemblyName = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateComInstanceFrom( assemblyName, typeName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Configuration.Assemblies.AssemblyHashAlgorithm>(L, 4)) 
                {
                    string assemblyName = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    byte[] hashValue = LuaAPI.lua_tobytes(L, 3);
                    System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm;translator.Get(L, 4, out hashAlgorithm);
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateComInstanceFrom( assemblyName, typeName, hashValue, hashAlgorithm );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Activator.CreateComInstanceFrom!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateInstanceFrom_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string assemblyFile = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstanceFrom( assemblyFile, typeName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object[]>(L, 3)) 
                {
                    string assemblyFile = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    object[] activationAttributes = (object[])translator.GetObject(L, 3, typeof(object[]));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstanceFrom( assemblyFile, typeName, activationAttributes );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.AppDomain>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.AppDomain domain = (System.AppDomain)translator.GetObject(L, 1, typeof(System.AppDomain));
                    string assemblyFile = LuaAPI.lua_tostring(L, 2);
                    string typeName = LuaAPI.lua_tostring(L, 3);
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstanceFrom( domain, assemblyFile, typeName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 9&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Reflection.BindingFlags>(L, 4)&& translator.Assignable<System.Reflection.Binder>(L, 5)&& translator.Assignable<object[]>(L, 6)&& translator.Assignable<System.Globalization.CultureInfo>(L, 7)&& translator.Assignable<object[]>(L, 8)&& translator.Assignable<System.Security.Policy.Evidence>(L, 9)) 
                {
                    string assemblyFile = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    bool ignoreCase = LuaAPI.lua_toboolean(L, 3);
                    System.Reflection.BindingFlags bindingAttr;translator.Get(L, 4, out bindingAttr);
                    System.Reflection.Binder binder = (System.Reflection.Binder)translator.GetObject(L, 5, typeof(System.Reflection.Binder));
                    object[] args = (object[])translator.GetObject(L, 6, typeof(object[]));
                    System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)translator.GetObject(L, 7, typeof(System.Globalization.CultureInfo));
                    object[] activationAttributes = (object[])translator.GetObject(L, 8, typeof(object[]));
                    System.Security.Policy.Evidence securityInfo = (System.Security.Policy.Evidence)translator.GetObject(L, 9, typeof(System.Security.Policy.Evidence));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstanceFrom( assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 10&& translator.Assignable<System.AppDomain>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Reflection.BindingFlags>(L, 5)&& translator.Assignable<System.Reflection.Binder>(L, 6)&& translator.Assignable<object[]>(L, 7)&& translator.Assignable<System.Globalization.CultureInfo>(L, 8)&& translator.Assignable<object[]>(L, 9)&& translator.Assignable<System.Security.Policy.Evidence>(L, 10)) 
                {
                    System.AppDomain domain = (System.AppDomain)translator.GetObject(L, 1, typeof(System.AppDomain));
                    string assemblyFile = LuaAPI.lua_tostring(L, 2);
                    string typeName = LuaAPI.lua_tostring(L, 3);
                    bool ignoreCase = LuaAPI.lua_toboolean(L, 4);
                    System.Reflection.BindingFlags bindingAttr;translator.Get(L, 5, out bindingAttr);
                    System.Reflection.Binder binder = (System.Reflection.Binder)translator.GetObject(L, 6, typeof(System.Reflection.Binder));
                    object[] args = (object[])translator.GetObject(L, 7, typeof(object[]));
                    System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)translator.GetObject(L, 8, typeof(System.Globalization.CultureInfo));
                    object[] activationAttributes = (object[])translator.GetObject(L, 9, typeof(object[]));
                    System.Security.Policy.Evidence securityAttributes = (System.Security.Policy.Evidence)translator.GetObject(L, 10, typeof(System.Security.Policy.Evidence));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstanceFrom( domain, assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Activator.CreateInstanceFrom!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateInstance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& translator.Assignable<System.ActivationContext>(L, 1)) 
                {
                    System.ActivationContext activationContext = (System.ActivationContext)translator.GetObject(L, 1, typeof(System.ActivationContext));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstance( activationContext );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& translator.Assignable<System.Type>(L, 1)) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    
                        object __cl_gen_ret = System.Activator.CreateInstance( type );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Type>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    bool nonPublic = LuaAPI.lua_toboolean(L, 2);
                    
                        object __cl_gen_ret = System.Activator.CreateInstance( type, nonPublic );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string assemblyName = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstance( assemblyName, typeName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.ActivationContext>(L, 1)&& translator.Assignable<string[]>(L, 2)) 
                {
                    System.ActivationContext activationContext = (System.ActivationContext)translator.GetObject(L, 1, typeof(System.ActivationContext));
                    string[] activationCustomData = (string[])translator.GetObject(L, 2, typeof(string[]));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstance( activationContext, activationCustomData );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count >= 1&& translator.Assignable<System.Type>(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    object[] args = translator.GetParams<object>(L, 2);
                    
                        object __cl_gen_ret = System.Activator.CreateInstance( type, args );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object[]>(L, 3)) 
                {
                    string assemblyName = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    object[] activationAttributes = (object[])translator.GetObject(L, 3, typeof(object[]));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstance( assemblyName, typeName, activationAttributes );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.AppDomain>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.AppDomain domain = (System.AppDomain)translator.GetObject(L, 1, typeof(System.AppDomain));
                    string assemblyName = LuaAPI.lua_tostring(L, 2);
                    string typeName = LuaAPI.lua_tostring(L, 3);
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstance( domain, assemblyName, typeName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Type>(L, 1)&& translator.Assignable<object[]>(L, 2)&& translator.Assignable<object[]>(L, 3)) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    object[] args = (object[])translator.GetObject(L, 2, typeof(object[]));
                    object[] activationAttributes = (object[])translator.GetObject(L, 3, typeof(object[]));
                    
                        object __cl_gen_ret = System.Activator.CreateInstance( type, args, activationAttributes );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& translator.Assignable<System.Type>(L, 1)&& translator.Assignable<System.Reflection.BindingFlags>(L, 2)&& translator.Assignable<System.Reflection.Binder>(L, 3)&& translator.Assignable<object[]>(L, 4)&& translator.Assignable<System.Globalization.CultureInfo>(L, 5)) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    System.Reflection.BindingFlags bindingAttr;translator.Get(L, 2, out bindingAttr);
                    System.Reflection.Binder binder = (System.Reflection.Binder)translator.GetObject(L, 3, typeof(System.Reflection.Binder));
                    object[] args = (object[])translator.GetObject(L, 4, typeof(object[]));
                    System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)translator.GetObject(L, 5, typeof(System.Globalization.CultureInfo));
                    
                        object __cl_gen_ret = System.Activator.CreateInstance( type, bindingAttr, binder, args, culture );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 6&& translator.Assignable<System.Type>(L, 1)&& translator.Assignable<System.Reflection.BindingFlags>(L, 2)&& translator.Assignable<System.Reflection.Binder>(L, 3)&& translator.Assignable<object[]>(L, 4)&& translator.Assignable<System.Globalization.CultureInfo>(L, 5)&& translator.Assignable<object[]>(L, 6)) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    System.Reflection.BindingFlags bindingAttr;translator.Get(L, 2, out bindingAttr);
                    System.Reflection.Binder binder = (System.Reflection.Binder)translator.GetObject(L, 3, typeof(System.Reflection.Binder));
                    object[] args = (object[])translator.GetObject(L, 4, typeof(object[]));
                    System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)translator.GetObject(L, 5, typeof(System.Globalization.CultureInfo));
                    object[] activationAttributes = (object[])translator.GetObject(L, 6, typeof(object[]));
                    
                        object __cl_gen_ret = System.Activator.CreateInstance( type, bindingAttr, binder, args, culture, activationAttributes );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 9&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Reflection.BindingFlags>(L, 4)&& translator.Assignable<System.Reflection.Binder>(L, 5)&& translator.Assignable<object[]>(L, 6)&& translator.Assignable<System.Globalization.CultureInfo>(L, 7)&& translator.Assignable<object[]>(L, 8)&& translator.Assignable<System.Security.Policy.Evidence>(L, 9)) 
                {
                    string assemblyName = LuaAPI.lua_tostring(L, 1);
                    string typeName = LuaAPI.lua_tostring(L, 2);
                    bool ignoreCase = LuaAPI.lua_toboolean(L, 3);
                    System.Reflection.BindingFlags bindingAttr;translator.Get(L, 4, out bindingAttr);
                    System.Reflection.Binder binder = (System.Reflection.Binder)translator.GetObject(L, 5, typeof(System.Reflection.Binder));
                    object[] args = (object[])translator.GetObject(L, 6, typeof(object[]));
                    System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)translator.GetObject(L, 7, typeof(System.Globalization.CultureInfo));
                    object[] activationAttributes = (object[])translator.GetObject(L, 8, typeof(object[]));
                    System.Security.Policy.Evidence securityInfo = (System.Security.Policy.Evidence)translator.GetObject(L, 9, typeof(System.Security.Policy.Evidence));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstance( assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 10&& translator.Assignable<System.AppDomain>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Reflection.BindingFlags>(L, 5)&& translator.Assignable<System.Reflection.Binder>(L, 6)&& translator.Assignable<object[]>(L, 7)&& translator.Assignable<System.Globalization.CultureInfo>(L, 8)&& translator.Assignable<object[]>(L, 9)&& translator.Assignable<System.Security.Policy.Evidence>(L, 10)) 
                {
                    System.AppDomain domain = (System.AppDomain)translator.GetObject(L, 1, typeof(System.AppDomain));
                    string assemblyName = LuaAPI.lua_tostring(L, 2);
                    string typeName = LuaAPI.lua_tostring(L, 3);
                    bool ignoreCase = LuaAPI.lua_toboolean(L, 4);
                    System.Reflection.BindingFlags bindingAttr;translator.Get(L, 5, out bindingAttr);
                    System.Reflection.Binder binder = (System.Reflection.Binder)translator.GetObject(L, 6, typeof(System.Reflection.Binder));
                    object[] args = (object[])translator.GetObject(L, 7, typeof(object[]));
                    System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)translator.GetObject(L, 8, typeof(System.Globalization.CultureInfo));
                    object[] activationAttributes = (object[])translator.GetObject(L, 9, typeof(object[]));
                    System.Security.Policy.Evidence securityAttributes = (System.Security.Policy.Evidence)translator.GetObject(L, 10, typeof(System.Security.Policy.Evidence));
                    
                        System.Runtime.Remoting.ObjectHandle __cl_gen_ret = System.Activator.CreateInstance( domain, assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Activator.CreateInstance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetObject_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<System.Type>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    string url = LuaAPI.lua_tostring(L, 2);
                    
                        object __cl_gen_ret = System.Activator.GetObject( type, url );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Type>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    string url = LuaAPI.lua_tostring(L, 2);
                    object state = translator.GetObject(L, 3, typeof(object));
                    
                        object __cl_gen_ret = System.Activator.GetObject( type, url, state );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Activator.GetObject!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
