using MvcApp.Framework;

namespace MvcApp.Messages
{
    public class CurrentMessageInfo
    {
        public SocketSendAdapter Connection { get; set; }
        public string Uri { get; set; }
    }
}