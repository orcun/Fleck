using System;
using Fleck;

namespace MvcApp.Framework
{
    public class SocketSendAdapter
    {
        private readonly WebSocketConnection _socket;

        public SocketSendAdapter(WebSocketConnection socket)
        {
            _socket = socket;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public void Send(string message)
        {
            _socket.Send(message);
        }

        public override bool Equals(object obj)
        {
            var websocket = obj as WebSocketConnection;
            if(websocket != null)
            {
                return websocket == _socket;
            }
            return base.Equals(obj);
        }

        public bool Equals(SocketSendAdapter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._socket, _socket);
        }

        public override int GetHashCode()
        {
            return (_socket != null ? _socket.GetHashCode() : 0);
        }
    }
}