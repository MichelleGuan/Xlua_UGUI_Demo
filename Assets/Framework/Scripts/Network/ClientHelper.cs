using UnityEngine;
using System.Collections.Generic;
using System.Net.Sockets;
using System;

using System.Threading;
using System.Net;
using System.Timers;
using System.IO;
using ProtoBuf;
using System.Collections;
using NetSocket;
 
public class ClientHelper
{
    // 开始时间
    private static long startTime;
    // 记录流量用
    public static float totalGetBytes;
    public static float totalSendBytes;

    /// <summary>
    /// 包头长度，用4个字节表示一个int包头
    /// </summary>
    public const int HEAD_LEN = 4;
    const int readSize = 4096;
    const int buffSize = 0xFFFF;

    private static Queue<byte[]> msgList = new Queue<byte[]>();
    private static TcpClient tcp;
    private static bool isConnected;
    private static byte[] readData = new byte[0xFFFF];
    private static int offSet = 0;
    private static int msgLength = 0;

    private delegate void ConnectionDelegate();
    public delegate void OnRpcDataDelegate(byte[] msg, int messageId);
    private delegate void OnErrorDelegate(string error);
    private delegate void OnDataWriteDelegate(byte[] msg);
    private delegate void DisConnectionDelegate();
    private static OnDataWriteDelegate _OnDataWrite;
    private static DisConnectionDelegate _OnDisConnect;
    private static OnErrorDelegate _OnError;

    public static OnRpcDataDelegate _OnRpcDataReceive;
    private static ConnectionDelegate _OnConnect;

    public int recon;

    private static Queue queue_sendMission;
    public static void InitConnect(string host, int port, OnRpcDataDelegate AddReceiveMission)
    {
        _OnRpcDataReceive = AddReceiveMission;
        try
        {
            queue_sendMission = new Queue();

            readData = new byte[buffSize];
            msgLength = 0;
            offSet = 0;
            tcp = new TcpClient();
            tcp.ReceiveBufferSize = 40960;
            tcp.SendBufferSize = 40960;
            tcp.Connect(host, port);

            isConnected = true;
            CallOnConnect();
            tcp.Client.BeginReceive(readData, 0, readSize, SocketFlags.None, new AsyncCallback(DoRead), tcp);
        }
        catch (Exception ex)
        {
            Debug.LogError("建立链接失败:"+ host+":"+port);
        }
    }

    //连接成功后读取数据
    private static void DoRead(IAsyncResult result)
    {
        if (isConnected == false)
        {
            return;
        }
        int length = 0;
        try
        {
            lock (tcp)
            {
                if (tcp.Client != null)
                {
                    length = tcp.Client.EndReceive(result);
                }
            }
            if (length < 1)
            {
                
                return;
            }

            ReadBuffInfo(length);
            lock (tcp)
            {
                if (tcp.Client != null)
                {
                    tcp.Client.BeginReceive(readData, offSet + msgLength, readSize, SocketFlags.None, new AsyncCallback(DoRead), tcp);//开始异步读取
                }
            }
        }
        catch (SocketException socketerror)
        {
            if (socketerror.SocketErrorCode != SocketError.ConnectionReset)
            {
                string errorMsg = "Error reading msg from socket: " + socketerror.Message + " ,, " + socketerror.SocketErrorCode + " ,, " + socketerror.StackTrace;
                //HandleError(errorMsg);
            }
        }
        catch (Exception generalError)
        {
            string errorMsg = "General socket on read: " + generalError.Message + " ,, " + generalError.StackTrace;
            //HandleError(errorMsg);
        }
    }

    /// <summary>
    /// 消息解码
    /// </summary>
    /// <param name="length"></param>
    private static void ReadBuffInfo(int length)
    {
        int PROTOLEN_BYTES = 4;//代表包头的int占用字节数
        byte[] edata = null;
        msgLength += length;
        while (true)
        {
            if (msgLength <= 0)
                break;
            MsgStream ms = new MsgStream();
            ms.SetReverse(true);
            ms.setReadIndex(offSet);
            if (ms.bytesAvailable(offSet + msgLength) >= PROTOLEN_BYTES)
            {
                int protolen = 0;
                ms.ReadInt(readData, ref protolen);//读取包头的4个字节
                if (ms.bytesAvailable(offSet + msgLength) >= protolen)
                {
                    int msgid = 0;
                    ms.ReadInt(readData, ref msgid);//读取代表协议号的4个字节
                    ms.Read(readData, offSet + PROTOLEN_BYTES + HEAD_LEN,
                        protolen - HEAD_LEN, ref edata);

                    CallOnRpcData(edata, msgid);//传出数据包
                    int progresslen = PROTOLEN_BYTES + protolen;
                    offSet += progresslen;
                    msgLength -= progresslen;
                    if (buffSize - offSet - msgLength < readSize)
                    {
                        Buffer.BlockCopy(readData, offSet, readData, 0, msgLength);
                        offSet = 0;
                    }
                }
                else
                {
                    if (buffSize - offSet - msgLength < readSize)
                    {
                        Buffer.BlockCopy(readData, offSet, readData, 0, msgLength);
                        offSet = 0;
                    }
                    break;
                }
            }
            else
            {
                if (buffSize - offSet - msgLength < readSize)
                {
                    Buffer.BlockCopy(readData, offSet, readData, 0, msgLength);
                    offSet = 0;
                }
                break;
            }
        } 
    }

    private static void CallOnRpcData(byte[] msg, int messageId)
    {
        if (_OnRpcDataReceive != null)
        {
            _OnRpcDataReceive(msg, messageId);
        }
    }

    //位移加密
    public static byte[] encryptForDis(byte[] data)
    {
        byte buff;
        for (int i = 0; i < data.Length; i += 5)
        {
            if (i + 3 > data.Length - 1) break;
            buff = (byte)~data[i + 2];
            data[i + 2] = data[i + 3];
            data[i + 3] = buff;
        }
        return data;
    }

    public static byte[] decryptForDis(byte[] data)
    {
        byte buff;
        for (int i = 0; i < data.Length; i += 5)
        {
            if (i + 3 > data.Length - 1) break;
            buff = data[i + 2];
            data[i + 2] = (byte)~data[i + 3];
            data[i + 3] = buff;
        }
        return data;
    }

    private static void CallOnConnect()
    {
        if (_OnConnect != null)
        {
            _OnConnect();
        }
    }
     
  
    /// <summary>
    /// 添加到发送列队
    /// </summary>
    /// <param name="request"></param>
    public static void addRequest(Request request)
    {
        //queue_sendMission.Enqueue(request);

        sendRequest(request);
    }

    /// <summary>
    /// 断开连接
    /// </summary>    
    public static void Disconnect()
    {
        try
        {
            isConnected = false;
            tcp.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("ex:"+ ex);
        }
    }

    private static void runRequestMission()
    {
        if (queue_sendMission != null && queue_sendMission.Count > 0)
        {
            Request request = queue_sendMission.Dequeue() as Request;
        }
    }

    public static bool sendRequest(Request request)
    {
         byte[] msg = MySerializerUtil.Serial(request);
        sendRequest(msg,request.msgType);
        return true;
    }

    public static bool sendRequest(byte[] msg,int msgType)
    {
        byte[] packetBytes = new byte[msg.Length + 8];

        // 添加包长
        int length = msg.Length + 4;

        packetBytes[0] = (byte)(length >> 24);//第一个字节存的高位，大端
        packetBytes[1] = (byte)(length >> 16);
        packetBytes[2] = (byte)(length >> 8);
        packetBytes[3] = (byte)(length);

        //Debug.Log(packetBytes[0]+","+ packetBytes[1] + ","+ packetBytes[2] + ","+ packetBytes[3]);

        //添加消息编号
        packetBytes[4] = (byte)(msgType >> 24);
        packetBytes[5] = (byte)(msgType >> 16);
        packetBytes[6] = (byte)(msgType >> 8);
        packetBytes[7] = (byte)(msgType);

        msg.CopyTo(packetBytes, 8);

        Write(packetBytes);
         
        totalSendBytes += packetBytes.Length;
        return true;
    }

    private static void Write(byte[] msg)
    {
        try
        {
            lock (msgList)//为了防止未知调用方的多线程调用，加入了线程同步
            {
                if (msgList.Count == 0)//保证顺序性
                {
                    msgList.Enqueue(msg);
                    tcp.Client.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(DoWrite), msg);
                }
                else
                {
                    msgList.Enqueue(msg);
                }
            }
        }
        catch (Exception ex)
        {
            string error = "Write exMsg on connection: " + ex.StackTrace + ex.Message + " " + ex.StackTrace;
            //Debug.LogError(error);
            //HandleError(error);
        }
    }

    private static void DoWrite(IAsyncResult msg)
    {
        if (isConnected == false)
        {
            //this.log.Trace("DoWrite --->  Server is not active.  Please start server and try again.      ");
            return;
        }
        //UnityEngine.DebugTrace.Log("DoWrite", Mogo.Util.LogLevel.INFO);
        SocketError socketError;
        byte[] bytes = (byte[])msg.AsyncState;
        int length = tcp.Client.EndSend(msg, out socketError);
        if (socketError == SocketError.Success)
        {
            try
            {
                lock (msgList)
                {
                    //通知
                    CallOnDataWrite(bytes);
                    //队列判断
                    if (msgList.Count > 0)
                    {
                        msgList.Dequeue();
                        if (msgList.Count > 0)//发送成功后队列中如果还有消息，则继续发送
                        {
                            byte[] firstMsg = msgList.Peek();
                            tcp.Client.BeginSend(firstMsg, 0, firstMsg.Length, SocketFlags.None, new AsyncCallback(DoWrite), firstMsg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               // HandleError("Revc Successs Error:      " + ex.ToString() + ex.StackTrace);
            }
            return;
        }
        if ((socketError != SocketError.WouldBlock) && (socketError != SocketError.InProgress))//发送失败，重试
        {
            try
            {
                lock (msgList)
                {
                    if (msgList.Count > 0)
                    {
                        byte[] firstMsg = msgList.Peek();
                        tcp.Client.BeginSend(firstMsg, 0, firstMsg.Length, SocketFlags.None, new AsyncCallback(DoWrite), firstMsg);
                    }
                }
            }
            catch (Exception ex)
            {
               // HandleError("SocketError.WouldBlock .      " + ex.ToString() + ex.StackTrace);
            }

            return;
        }


        try
        {
            throw new SocketException((int)socketError);
        }
        catch (SocketException ex)
        {
            //HandleError(" DoWrite .      " + ex.ToString() + ex.StackTrace);
        }
    }

    private static void CallOnDataWrite(byte[] data)
    {
        if (_OnDataWrite != null)
        {
            _OnDataWrite(data);
        }
    }
}
