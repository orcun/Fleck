using MvcApp.Framework;

namespace MvcApp.Messages
{
    [Subscribe("/canvas")]
    public class CanvasMessage : ISocketMessage
    {
        public SocketSendAdapter Socket { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public string Name { get; set; }
    }
}