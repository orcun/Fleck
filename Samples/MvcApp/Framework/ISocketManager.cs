using Fleck;

namespace MvcApp.Framework
{
    public interface ISocketManager
    {
        void Add(WebSocketConnection socket);
        void Remove(WebSocketConnection socket);
        void Reply(object message);
        void Publish(object message);
        void PublishExcept(object message, SocketSendAdapter socket);
        void MessageBegin(WebSocketConnection socket);
        void MessageEnd();
    }
}