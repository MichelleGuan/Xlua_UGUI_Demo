using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WT.UI;
using System;

public class mainControl{
    private mainUI _main;
    public event Action _mainPanelEvent;
    public mainControl()
    {
        _main = new mainUI();
        _main._mainPanelEvent += JudgeNull;
    }
	public void ShowMain()
    {
        WTUIPage.ShowPage("_uiMain", _main);
    }
    public void JudgeNull()
    {
        if (_mainPanelEvent != null)
            _mainPanelEvent();
    }
}
