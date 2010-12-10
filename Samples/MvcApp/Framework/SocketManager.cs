using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Fleck;
using MvcApp.Messages;

namespace MvcApp.Framework
{
    public class SocketManager : ISocketManager
    {
        private readonly IList<WebSocketConnection> _allSockets;

        [ThreadStatic]
        private static CurrentMessageInfo _currentInfo;

        public SocketManager()
        {
            _allSockets = new List<WebSocketConnection>();
        }

        public void Add(WebSocketConnection socket)
        {
            _allSockets.Add(socket);
        }

        public void MessageBegin(WebSocketConnection socket)
        {
            _currentInfo = new CurrentMessageInfo {Connection = new SocketSendAdapter(socket)};
        }

        public void MessageEnd()
        {
            _currentInfo = null;
        }

        public void Remove(WebSocketConnection socket)
        {
            _allSockets.Remove(socket);
        }

        public void Reply(object message)
        {
            var serializer = new JavaScriptSerializer();
            var sJson = serializer.Serialize(message);
            _currentInfo.Connection.Send(sJson);
        }

        public void Publish(object message)
        {
            var serializer = new JavaScriptSerializer();
            var wireMessage = new WireResponseMessage {Uri = message.GetType().FullName.Replace('.', '_'), Data = message};
            var sJson = serializer.Serialize(wireMessage);
            foreach (var socket in _allSockets.ToList())
            {
                socket.Send(sJson);
            }
        }

    }
}