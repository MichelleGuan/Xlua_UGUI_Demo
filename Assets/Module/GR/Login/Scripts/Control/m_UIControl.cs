using System;
using UnityEngine;
using WT.UI;
using System.Collections.Generic;
using XLua;
[Hotfix]
[LuaCallCSharp]
public class m_UIControl
{
    public m_UI _uilogin { get; set; }
    //public event Action OnRegister;
    #region  事件
    public Action<m_UIControl> LoginEvent;
    public void AddLoginEvent(Action<m_UIControl> method)
    {
        LoginEvent = method;
    }
    public void CallLoginEvent(string account, string password)
    {
        List<User> users = new List<User>() { new User("10", "10"), new User("2", "2") };
        foreach (User thisUser in users)
        {
            if (account == thisUser.name && password == thisUser.password)
            {
                LoginEvent(this);
                _uilogin.Hide();
            }
            else
            {
                _uilogin.LoginFail();
            }
        }
    }
    #endregion

    private byte[] _hotfixLoginControlBytes;
    private String _hotfixLoginControlFile = "GR/Login/HotfixLoginControl.lua";
    private byte[] _luaLoginControlBytes;
    private String _luaLoginControlScritp = "GR/Login/LuaLoginControl.lua";
    private LuaTable _luaLoginControl;
    private Action _luaInitControl;

    public m_UIControl()
    {
        _uilogin = new m_UI();
        _hotfixLoginControlBytes = FileIO.CustomLoaderMethod(ref _hotfixLoginControlFile);
        _luaLoginControlBytes = FileIO.CustomLoaderMethod(ref _luaLoginControlScritp);

#if HOTFIX_ENABLE
        XLuaUtil.GlobLuaEnv.DoString(_hotfixLoginControlBytes);
#endif

        _luaLoginControl = XLuaUtil.GlobLuaEnv.NewTable();
        LuaTable meta = XLuaUtil.GlobLuaEnv.NewTable();
        meta.Set("__index", XLuaUtil.GlobLuaEnv.Global);
        _luaLoginControl.SetMetaTable(meta);
        meta.Dispose();

        _luaLoginControl.Set("self", this);
        XLuaUtil.GlobLuaEnv.DoString(_luaLoginControlBytes, "LuaLogin", _luaLoginControl);
        _luaLoginControl.Get("init_control", out _luaInitControl);
        _initControl();
    }


    private void _initControl()
    {
        if (_luaInitControl != null)
        {
            _luaInitControl();
        }
    }

    public void ShowLoginUI()
    {
        WTUIPage.ShowPage("XluaUILogin", _uilogin);
    }
}
   

    /*public m_UIControl()
    {
        _uilogin = new m_UI();
        _uilogin.AddEventLogin(_btnLoginCallBack);
        //_uilogin.OnRegister += JudgeNull;
    }
    public void ShowLoginUI()
    {
        WTUIPage.ShowPage("uiLogin", _uilogin);
    }
    
    
    //public void JudgeNull()
    //{
    //    if (OnRegister != null)
    //        OnRegister();
    //}
}*/