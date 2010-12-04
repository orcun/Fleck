using MvcApp.Framework;

namespace MvcApp.Messages
{
    [Subscribe("/canvas")]
    public class CanvasMessage : ISocketMessage
    {
        public SocketSendAdapter Socket { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}