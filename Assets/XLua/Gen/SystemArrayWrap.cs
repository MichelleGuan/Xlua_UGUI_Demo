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
    public class SystemArrayWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Array);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 7, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLength", _m_GetLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLongLength", _m_GetLongLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLowerBound", _m_GetLowerBound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetValue", _m_GetValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetValue", _m_SetValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUpperBound", _m_GetUpperBound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clone", _m_Clone);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CopyTo", _m_CopyTo);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Length", _g_get_Length);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LongLength", _g_get_LongLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Rank", _g_get_Rank);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSynchronized", _g_get_IsSynchronized);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SyncRoot", _g_get_SyncRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsFixedSize", _g_get_IsFixedSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsReadOnly", _g_get_IsReadOnly);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 10, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateInstance", _m_CreateInstance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "BinarySearch", _m_BinarySearch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Clear", _m_Clear_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Copy", _m_Copy_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IndexOf", _m_IndexOf_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LastIndexOf", _m_LastIndexOf_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Reverse", _m_Reverse_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Sort", _m_Sort_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConstrainedCopy", _m_ConstrainedCopy_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.Array does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetLength( dimension );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLongLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        long __cl_gen_ret = __cl_gen_to_be_invoked.GetLongLength( dimension );
                        LuaAPI.lua_pushint64(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLowerBound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetLowerBound( dimension );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( index );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))) 
                {
                    long index = LuaAPI.lua_toint64(L, 2);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( index );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int index1 = LuaAPI.xlua_tointeger(L, 2);
                    int index2 = LuaAPI.xlua_tointeger(L, 3);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( index1, index2 );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    long index1 = LuaAPI.lua_toint64(L, 2);
                    long index2 = LuaAPI.lua_toint64(L, 3);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( index1, index2 );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    int index1 = LuaAPI.xlua_tointeger(L, 2);
                    int index2 = LuaAPI.xlua_tointeger(L, 3);
                    int index3 = LuaAPI.xlua_tointeger(L, 4);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( index1, index2, index3 );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))) 
                {
                    long index1 = LuaAPI.lua_toint64(L, 2);
                    long index2 = LuaAPI.lua_toint64(L, 3);
                    long index3 = LuaAPI.lua_toint64(L, 4);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( index1, index2, index3 );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    int[] indices = translator.GetParams<int>(L, 2);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( indices );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2)))) 
                {
                    long[] indices = translator.GetParams<long>(L, 2);
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.GetValue( indices );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.GetValue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    long index = LuaAPI.lua_toint64(L, 3);
                    
                    __cl_gen_to_be_invoked.SetValue( value, index );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    int index = LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.SetValue( value, index );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    long index1 = LuaAPI.lua_toint64(L, 3);
                    long index2 = LuaAPI.lua_toint64(L, 4);
                    
                    __cl_gen_to_be_invoked.SetValue( value, index1, index2 );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    int index1 = LuaAPI.xlua_tointeger(L, 3);
                    int index2 = LuaAPI.xlua_tointeger(L, 4);
                    
                    __cl_gen_to_be_invoked.SetValue( value, index1, index2 );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) || LuaAPI.lua_isint64(L, 5))) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    long index1 = LuaAPI.lua_toint64(L, 3);
                    long index2 = LuaAPI.lua_toint64(L, 4);
                    long index3 = LuaAPI.lua_toint64(L, 5);
                    
                    __cl_gen_to_be_invoked.SetValue( value, index1, index2, index3 );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    int index1 = LuaAPI.xlua_tointeger(L, 3);
                    int index2 = LuaAPI.xlua_tointeger(L, 4);
                    int index3 = LuaAPI.xlua_tointeger(L, 5);
                    
                    __cl_gen_to_be_invoked.SetValue( value, index1, index2, index3 );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 2&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3))) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    int[] indices = translator.GetParams<int>(L, 3);
                    
                    __cl_gen_to_be_invoked.SetValue( value, indices );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 2&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3)))) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    long[] indices = translator.GetParams<long>(L, 3);
                    
                    __cl_gen_to_be_invoked.SetValue( value, indices );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.SetValue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEnumerator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.IEnumerator __cl_gen_ret = __cl_gen_to_be_invoked.GetEnumerator(  );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUpperBound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetUpperBound( dimension );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateInstance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<System.Type>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    System.Type elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int length = LuaAPI.xlua_tointeger(L, 2);
                    
                        System.Array __cl_gen_ret = System.Array.CreateInstance( elementType, length );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Type>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Type elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int length1 = LuaAPI.xlua_tointeger(L, 2);
                    int length2 = LuaAPI.xlua_tointeger(L, 3);
                    
                        System.Array __cl_gen_ret = System.Array.CreateInstance( elementType, length1, length2 );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<System.Type>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Type elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int length1 = LuaAPI.xlua_tointeger(L, 2);
                    int length2 = LuaAPI.xlua_tointeger(L, 3);
                    int length3 = LuaAPI.xlua_tointeger(L, 4);
                    
                        System.Array __cl_gen_ret = System.Array.CreateInstance( elementType, length1, length2, length3 );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count >= 1&& translator.Assignable<System.Type>(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    System.Type elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int[] lengths = translator.GetParams<int>(L, 2);
                    
                        System.Array __cl_gen_ret = System.Array.CreateInstance( elementType, lengths );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count >= 1&& translator.Assignable<System.Type>(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2)))) 
                {
                    System.Type elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    long[] lengths = translator.GetParams<long>(L, 2);
                    
                        System.Array __cl_gen_ret = System.Array.CreateInstance( elementType, lengths );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Type>(L, 1)&& translator.Assignable<int[]>(L, 2)&& translator.Assignable<int[]>(L, 3)) 
                {
                    System.Type elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int[] lengths = (int[])translator.GetObject(L, 2, typeof(int[]));
                    int[] lowerBounds = (int[])translator.GetObject(L, 3, typeof(int[]));
                    
                        System.Array __cl_gen_ret = System.Array.CreateInstance( elementType, lengths, lowerBounds );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.CreateInstance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BinarySearch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    
                        int __cl_gen_ret = System.Array.BinarySearch( array, value );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    int length = LuaAPI.xlua_tointeger(L, 3);
                    object value = translator.GetObject(L, 4, typeof(object));
                    
                        int __cl_gen_ret = System.Array.BinarySearch( array, index, length, value );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& translator.Assignable<System.Collections.IComparer>(L, 3)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    System.Collections.IComparer comparer = (System.Collections.IComparer)translator.GetObject(L, 3, typeof(System.Collections.IComparer));
                    
                        int __cl_gen_ret = System.Array.BinarySearch( array, value, comparer );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<System.Collections.IComparer>(L, 5)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    int length = LuaAPI.xlua_tointeger(L, 3);
                    object value = translator.GetObject(L, 4, typeof(object));
                    System.Collections.IComparer comparer = (System.Collections.IComparer)translator.GetObject(L, 5, typeof(System.Collections.IComparer));
                    
                        int __cl_gen_ret = System.Array.BinarySearch( array, index, length, value, comparer );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.BinarySearch!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    int length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Clear( array, index, length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clone(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        object __cl_gen_ret = __cl_gen_to_be_invoked.Clone(  );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Copy_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array destinationArray = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Copy( sourceArray, destinationArray, length );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    System.Array sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array destinationArray = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    long length = LuaAPI.lua_toint64(L, 3);
                    
                    System.Array.Copy( sourceArray, destinationArray, length );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Array>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    System.Array sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int sourceIndex = LuaAPI.xlua_tointeger(L, 2);
                    System.Array destinationArray = (System.Array)translator.GetObject(L, 3, typeof(System.Array));
                    int destinationIndex = LuaAPI.xlua_tointeger(L, 4);
                    int length = LuaAPI.xlua_tointeger(L, 5);
                    
                    System.Array.Copy( sourceArray, sourceIndex, destinationArray, destinationIndex, length );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))&& translator.Assignable<System.Array>(L, 3)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) || LuaAPI.lua_isint64(L, 5))) 
                {
                    System.Array sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    long sourceIndex = LuaAPI.lua_toint64(L, 2);
                    System.Array destinationArray = (System.Array)translator.GetObject(L, 3, typeof(System.Array));
                    long destinationIndex = LuaAPI.lua_toint64(L, 4);
                    long length = LuaAPI.lua_toint64(L, 5);
                    
                    System.Array.Copy( sourceArray, sourceIndex, destinationArray, destinationIndex, length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.Copy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IndexOf_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    
                        int __cl_gen_ret = System.Array.IndexOf( array, value );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    int startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int __cl_gen_ret = System.Array.IndexOf( array, value, startIndex );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    int startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int __cl_gen_ret = System.Array.IndexOf( array, value, startIndex, count );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.IndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Initialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LastIndexOf_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    
                        int __cl_gen_ret = System.Array.LastIndexOf( array, value );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    int startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int __cl_gen_ret = System.Array.LastIndexOf( array, value, startIndex );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object value = translator.GetObject(L, 2, typeof(object));
                    int startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int __cl_gen_ret = System.Array.LastIndexOf( array, value, startIndex, count );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.LastIndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reverse_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& translator.Assignable<System.Array>(L, 1)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    
                    System.Array.Reverse( array );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    int length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Reverse( array, index, length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.Reverse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Sort_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& translator.Assignable<System.Array>(L, 1)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    
                    System.Array.Sort( array );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    int length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Sort( array, index, length );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)) 
                {
                    System.Array keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    
                    System.Array.Sort( keys, items );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Collections.IComparer>(L, 2)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Collections.IComparer comparer = (System.Collections.IComparer)translator.GetObject(L, 2, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( array, comparer );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Array keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 3);
                    int length = LuaAPI.xlua_tointeger(L, 4);
                    
                    System.Array.Sort( keys, items, index, length );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Collections.IComparer>(L, 4)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    int length = LuaAPI.xlua_tointeger(L, 3);
                    System.Collections.IComparer comparer = (System.Collections.IComparer)translator.GetObject(L, 4, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( array, index, length, comparer );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& translator.Assignable<System.Collections.IComparer>(L, 3)) 
                {
                    System.Array keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    System.Collections.IComparer comparer = (System.Collections.IComparer)translator.GetObject(L, 3, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( keys, items, comparer );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Collections.IComparer>(L, 5)) 
                {
                    System.Array keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 3);
                    int length = LuaAPI.xlua_tointeger(L, 4);
                    System.Collections.IComparer comparer = (System.Collections.IComparer)translator.GetObject(L, 5, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( keys, items, index, length, comparer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.Sort!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int index = LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.CopyTo( array, index );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<System.Array>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    System.Array array = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    long index = LuaAPI.lua_toint64(L, 3);
                    
                    __cl_gen_to_be_invoked.CopyTo( array, index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.CopyTo!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConstrainedCopy_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Array sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int sourceIndex = LuaAPI.xlua_tointeger(L, 2);
                    System.Array destinationArray = (System.Array)translator.GetObject(L, 3, typeof(System.Array));
                    int destinationIndex = LuaAPI.xlua_tointeger(L, 4);
                    int length = LuaAPI.xlua_tointeger(L, 5);
                    
                    System.Array.ConstrainedCopy( sourceArray, sourceIndex, destinationArray, destinationIndex, length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Length);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LongLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, __cl_gen_to_be_invoked.LongLength);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Rank(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Rank);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSynchronized(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsSynchronized);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SyncRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, __cl_gen_to_be_invoked.SyncRoot);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsFixedSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsFixedSize);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsReadOnly(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array __cl_gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsReadOnly);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
