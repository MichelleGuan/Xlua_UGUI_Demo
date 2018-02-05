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
    public class FileIOWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FileIO);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 19, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadPrefab", _m_LoadPrefab_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadDependencies", _m_LoadDependencies_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadAssetBundle", _m_LoadAssetBundle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadAssetBundleManifest", _m_LoadAssetBundleManifest_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteFile", _m_WriteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteAllAbName", _m_WriteAllAbName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteLua", _m_WriteLua_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteLuaPb", _m_WriteLuaPb_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileMd5", _m_GetFileMd5_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadPB", _m_LoadPB_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFile", _m_ReadFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFileSbyte", _m_ReadFileSbyte_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CustomLoaderMethod", _m_CustomLoaderMethod_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsExists", _m_IsExists_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PathURL", FileIO.PathURL);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HOTFIX_LUA_PATH", FileIO.HOTFIX_LUA_PATH);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HOTFIX_LUA_PB_PATH", FileIO.HOTFIX_LUA_PB_PATH);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Remote_AssetBunlde_PATH", FileIO.Remote_AssetBunlde_PATH);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FileIO __cl_gen_ret = new FileIO();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FileIO constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadPrefab_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    int abIndex = LuaAPI.xlua_tointeger(L, 1);
                    
                        UnityEngine.GameObject __cl_gen_ret = FileIO.LoadPrefab( abIndex );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string abName = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.GameObject __cl_gen_ret = FileIO.LoadPrefab( abName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FileIO.LoadPrefab!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadDependencies_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string name = LuaAPI.lua_tostring(L, 1);
                    
                        System.Collections.Generic.List<UnityEngine.AssetBundle> __cl_gen_ret = FileIO.LoadDependencies( name );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<UnityEngine.AssetBundleManifest>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityEngine.AssetBundleManifest abm = (UnityEngine.AssetBundleManifest)translator.GetObject(L, 1, typeof(UnityEngine.AssetBundleManifest));
                    string abName = LuaAPI.lua_tostring(L, 2);
                    
                        System.Collections.Generic.List<UnityEngine.AssetBundle> __cl_gen_ret = FileIO.LoadDependencies( abm, abName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FileIO.LoadDependencies!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAssetBundle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string abName = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.AssetBundle __cl_gen_ret = FileIO.LoadAssetBundle( abName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAssetBundleManifest_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        UnityEngine.AssetBundleManifest __cl_gen_ret = FileIO.LoadAssetBundleManifest(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    string fileName = LuaAPI.lua_tostring(L, 2);
                    byte[] data = LuaAPI.lua_tobytes(L, 3);
                    
                    FileIO.WriteFile( filePath, fileName, data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAllAbName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    string fileName = LuaAPI.lua_tostring(L, 2);
                    string text = LuaAPI.lua_tostring(L, 3);
                    
                    FileIO.WriteAllAbName( filePath, fileName, text );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteLua_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    byte[] data = LuaAPI.lua_tobytes(L, 2);
                    
                    FileIO.WriteLua( fileName, data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteLuaPb_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    byte[] data = LuaAPI.lua_tobytes(L, 2);
                    
                    FileIO.WriteLuaPb( fileName, data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileMd5_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = FileIO.GetFileMd5( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string fileName = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = FileIO.GetFileMd5( path, fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FileIO.GetFileMd5!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadPB_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = FileIO.LoadPB( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string fileName = LuaAPI.lua_tostring(L, 2);
                    
                        byte[] __cl_gen_ret = FileIO.ReadFile( path, fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFileSbyte_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string fileName = LuaAPI.lua_tostring(L, 2);
                    
                        sbyte[] __cl_gen_ret = FileIO.ReadFileSbyte( path, fileName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CustomLoaderMethod_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = FileIO.CustomLoaderMethod( ref fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    LuaAPI.lua_pushstring(L, fileName);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsExists_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = FileIO.IsExists( filePath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
