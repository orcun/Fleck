using System;

namespace Fleck
{
    public interface IWebSocketConnection
    {
        Action OnOpen { get; set; }
        Action OnClose { get; set; }
        Action<string> OnMessage { get; set; }
        Action<byte[]> OnBinary { get; set; }
        Action<Exception> OnError { get; set; }
        void Send(string message, string logMessage = null);
        void Send(byte[] message, string logMessage = null);
        void Close();
        IWebSocketConnectionInfo ConnectionInfo { get; }
        bool IsAvailable { get; }
    }
}
