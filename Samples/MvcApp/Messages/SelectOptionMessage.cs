using MvcApp.Framework;

namespace MvcApp.Messages
{
    [Subscribe("/select")]
    public class SelectOptionMessage : ISocketMessage
    {
        public SocketSendAdapter Socket { get; set; }
        public bool Yes { get; set; }
    }

    public class CurrentMessageInfo
    {
        public SocketSendAdapter Connection { get; set; }
        public string Uri { get; set; }
    }
}