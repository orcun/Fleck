using MvcApp.Framework;

namespace MvcApp.Messages
{
    [Subscribe("/chat")]
    public class ChatMessage : ISocketMessage
    {
        public SocketSendAdapter Socket { get; set; }
        public string Text { get; set; }
    }
}