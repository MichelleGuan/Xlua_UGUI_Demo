using System;
using UnityEngine;
using WT.UI;

public class m_bpControl
{
    private m_bp _uiBP;
    public event Action _backpackEvent;
    public m_bpControl()
    {
        _uiBP = new m_bp();
        _uiBP._eventBackpack += M_BackPackEvent;
    }
    public void ShowBackpack()
    {
        WTUIPage.ShowPage("uiBackpack", _uiBP);
    }
    private void M_BackPackEvent()
    {
        if (_backpackEvent != null)
        {
            _backpackEvent();
        }
    }
}