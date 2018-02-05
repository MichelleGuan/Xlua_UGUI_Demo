using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WT.UI;

public class m_GameMainCanvas:BaseCanvas,IHandlerReceive
{
    private m_UIControl _uicontrol;
    private mainControl _main;
    private m_bpControl _bp;
    void Awake()
    {
        AddHandlerReceiveEvent(this);//注册
        _uicontrol = new m_UIControl();
        _main = new mainControl();
        _bp = new m_bpControl();
        _uicontrol.AddLoginEvent(_loginEventCallBack);
        _main._mainPanelEvent += _showBackpack;
       // _uicontrol.OnRegister += _onRegister;
        _showLogin();
    }

    //接收服务器消息
    public Response RunServerReceive()
    {
        return null;
    }

    protected override void RunUpdate()
    {
    }
    private void _showBackpack()
    {
        _bp.ShowBackpack();
    }
    //显示登录界面
    private void _showLogin()
    {
        _uicontrol.ShowLoginUI();
    }
    #region  _loginControl 事件回调
    private void _loginEventCallBack(m_UIControl control)
    {
        _main.ShowMain();
    }
    #endregion
    private void _onRegister()
    {
        _uicontrol._uilogin.Hide();
    }
   
}