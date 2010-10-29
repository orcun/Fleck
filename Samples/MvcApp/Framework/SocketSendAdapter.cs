using Fleck;

namespace MvcApp.Framework
{
    public class SocketSendAdapter
    {
        private readonly WebSocketConnection _socket;

        public SocketSendAdapter(WebSocketConnection socket)
        {
            _socket = socket;
        }
    }
}