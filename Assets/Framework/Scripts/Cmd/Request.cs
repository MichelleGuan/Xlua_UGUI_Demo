using ProtoBuf;

[ProtoContract]
public class Request
{
    [ProtoMember(1)]
    public int msgType;
 
    public Request()
    { }

     
}


