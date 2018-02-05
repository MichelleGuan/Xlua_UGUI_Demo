using ProtoBuf;

[ProtoContract]
//[ProtoInclude(10, typeof(TestResponse))]

public class Response
{
    // 统一返回码类型
    public const int SUCCESS = 1000; // 成功
    public const int FAILED = 1001; // 失败
    public const int ERROR_NO_GOLD = 1002; // 玩家金币不够
    public const int ERROR_NO_CREDITS = 1003; // 玩家点券不够
    public const int ERROR_NO_LOVE_PT = 1004; // 玩家友情值不够
    public const int ERROR_OUT_OF_LIMIT = 1005; // 超越了上限值
    public const int ERROR_CANT_UPDATE_CURRENCY = 1006; // 无法扣除游戏币
    public const int ERROR_WRONG_PARAM = 1007; // 玩家参数有误
    public const int ERROR_UNKNOWN = 1008; // 未知错误
    public const int ERROR_NEED_RELOGIN = 1009; // 需要重新登录
    public const int ERROR_NOT_AVAILABLE = 1010; // 目前不可用
    public const int ERROR_CANT_UPDATASQL = 1011;//更新数据库不成功

    [ProtoMember(1)]
    public int msgType;
 
    [ProtoMember(2)]
    public byte[] data;
    public Response()
    {
    }

    public Response(int msgType,byte[] data)
    {
        this.msgType = msgType;
        this.data = data;
    }
 
    
}

