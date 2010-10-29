namespace MvcApp.Framework
{
    public interface ISocketMessage
    {
        SocketSendAdapter Socket { get; set; }
    }
}