using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WT.UI;
using System;

public class mainUI : WTUIPage {
    public event Action _mainPanelEvent;
    public mainUI() :base(UIType.Fixed, UIMode.DoNothing, UICollider.None)
    {
        uiIndex = R.Prefab.MAINVIEW;
    }
    public override void Awake(GameObject go)
    {
        this.gameObject.transform.Find("Content/Icon").GetComponent<Button>().onClick.AddListener(
            () =>
            {
                if (_mainPanelEvent != null)
                {
                    _mainPanelEvent();
                }
            }
            );
    }
}
