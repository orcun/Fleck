using MvcApp.Framework;

namespace MvcApp.Messages
{
    [Subscribe("/select")]
    public class SelectOptionMessage : ISocketMessage
    {
        public SocketSendAdapter Socket { get; set; }
        public bool Yes { get; set; }
    }
}