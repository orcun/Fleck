using Fleck;

namespace MvcApp.Framework
{
    public interface ISocketManager
    {
        void Add(WebSocketConnection socket);
        void Remove(WebSocketConnection socket);
        void Publish(object obj);
    }
}