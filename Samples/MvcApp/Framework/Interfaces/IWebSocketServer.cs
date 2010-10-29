namespace MvcApp.Framework
{
    public interface IWebSocketServer
    {
        void Publish(object obj);
    }
}