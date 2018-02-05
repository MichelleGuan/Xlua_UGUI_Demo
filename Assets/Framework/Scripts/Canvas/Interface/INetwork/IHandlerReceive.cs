public interface IHandlerReceive
{
    /// <summary>
    /// 处理服务器返回的回调数据,如果数据在c#中被处理，则返回null，否则返回该Response数据，交由lua处理
    /// </summary>
    /// <returns></returns>
    Response RunServerReceive();
}