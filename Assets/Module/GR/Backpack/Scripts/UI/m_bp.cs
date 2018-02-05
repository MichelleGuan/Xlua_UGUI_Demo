using System;
using UnityEngine;
using UnityEngine.UI;
using WT.UI;

public class m_bp : WTUIPage
{
    public event Action _eventBackpack;
    public m_bp()
        : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiIndex = R.Prefab.BACKPACK;
    }

    public override void Awake(GameObject go)
    {
        this.gameObject.transform.Find("Content/BackButton").GetComponent<Button>().onClick.AddListener(_onClickBackCall);
    }
    private void _onClickBackCall()
    {
        if (_eventBackpack != null)
        {
            Hide();
            _eventBackpack();
        }
    }
}