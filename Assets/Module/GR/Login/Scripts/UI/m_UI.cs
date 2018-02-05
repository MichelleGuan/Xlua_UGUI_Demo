using System;
using UnityEngine;
using UnityEngine.UI;
using WT.UI;
using XLua;
[Hotfix]
[LuaCallCSharp]
public class m_UI :WTUIPage
{
    private byte[] _hotfixLoginBytes;
    private String _hotfixLoginFile = "GR/Login/HotfixLogin.lua";
    private byte[] _luaLoginBytes;
    private String _luaLoginScritp = "GR/Login/LuaLogin.lua";
    private LuaTable _luaLogin;
    private Action _luaInitUI;
    private GameObject label_ac, label_pw, hint;
    public event Action OnRegister; 
    
    public m_UI(): base(UIType.Fixed,UIMode.DoNothing,UICollider.None)
    {
        uiIndex = R.Prefab.MLOGIN;
        _hotfixLoginBytes = FileIO.CustomLoaderMethod(ref _hotfixLoginFile);
        _luaLoginBytes = FileIO.CustomLoaderMethod(ref _luaLoginScritp);
    }
    #region  事件
    public Action<string, string> EventLogin= (account,password) =>
    {
        
    };

    public void CallEventLogin(string account, string password)
    {
        if (this.EventLogin != null)
        {
            EventLogin(account, password);
        }
    }

    #endregion
    public override void Awake(GameObject go)
    {
#if HOTFIX_ENABLE
        XLuaUtil.GlobLuaEnv.DoString(_hotfixLoginBytes);
#endif

        _luaLogin = XLuaUtil.GlobLuaEnv.NewTable();

        LuaTable meta = XLuaUtil.GlobLuaEnv.NewTable();
        meta.Set("__index", XLuaUtil.GlobLuaEnv.Global);
        _luaLogin.SetMetaTable(meta);
        meta.Dispose();

        _luaLogin.Set("self", this);
        XLuaUtil.GlobLuaEnv.DoString(_luaLoginBytes, "LuaLogin", _luaLogin);
        _luaLogin.Get("init_ui", out _luaInitUI);
        _initUI();
    }

    private void _initUI()
    {
        if (_luaInitUI != null)
        {
            _luaInitUI();
        }
    }
    public void LoginFail()
    {
        transform.Find("Content/Loginregister/Hint").gameObject.SetActive(true);
    }
}