using System;
using UnityEngine;

/// <summary>
/// canvas基类，所有继承类不可重写MonoBehaviour->Update
/// 
/// </summary>
public abstract class BaseCanvas : MonoBehaviour
{
    protected int state;
    
    private IHandlerReceive handlerReceive;

    /// <summary>
    /// 处理逻辑，在update中被调用
    /// </summary>
    protected abstract void RunUpdate();
   
    /// <summary>
    /// 注册回调事件
    /// </summary>
    /// <param name="canvas"></param>
    protected void AddHandlerReceiveEvent(IHandlerReceive canvas)
    {
        handlerReceive = canvas;
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="state"></param>
    protected void SetState(int state)
    {
        this.state = state;
    }
     
    /// <summary>
    /// 发送数据给服务器
    /// </summary>
    /// <param name="request"></param>
    protected static void SendMessage(Request request)
    {
        Debug.Log("发送消息:" + request);
        NetWorkClient.sendRequest(request);
    }

    void Update()
    {
        RunServerReceive();
        RunUpdate();
         
    }
    
    /// <summary>
    /// 获得网络返回数据，如不为空
    /// </summary>
    /// <returns></returns>
    protected static Response Response
    {
        get
        {
            return NetWorkClient.NextMission();
        }
     }
     
    private void RunServerReceive()
    {
        if (handlerReceive != null)
            handlerReceive.RunServerReceive();
    }
}

