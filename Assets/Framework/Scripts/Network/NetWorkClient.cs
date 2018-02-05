
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 功能示例：
/// 
/// </summary>
public class NetWorkClient
{
    private static Queue<Response> queue_receiveMission = new Queue<Response>();

    /// <summary>
    /// 初始化链接
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public static void init(string ip, int port)
    {
        queue_receiveMission.Clear();
        ClientHelper.InitConnect(ip, port, AddReceiveMission);
    }

    /// <summary>
    /// 如服务器有返回数据，取出服务器返回数据的第一个
    /// </summary>
    /// <returns></returns>
    public static Response NextMission()
    {
        if (queue_receiveMission != null && queue_receiveMission.Count > 0)
        {
            return queue_receiveMission.Dequeue();
        }
        return null;
    }

    /// <summary>
    /// 获取服务器回调数据
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msgType"></param>
    private static void AddReceiveMission(byte[] data, int msgType)
    {
        Response response = new Response(msgType, data);
        queue_receiveMission.Enqueue(response);
    }

    /// <summary>
    /// 向服务器发送一个request
    /// </summary>
    /// <param name="request"></param>
    public static void sendRequest(Request request)
    {
        ClientHelper.addRequest(request);
    }

    /// <summary>
    /// 当app退出时必须关闭链接
    /// </summary>
    public static void Disconnect()
    {
        ClientHelper.Disconnect();
    }
}
