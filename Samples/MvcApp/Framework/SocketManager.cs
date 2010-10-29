using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Fleck;

namespace MvcApp.Framework
{
    public class SocketManager : ISocketManager
    {
        private readonly IList<WebSocketConnection> _allSockets;

        public SocketManager()
        {
            _allSockets = new List<WebSocketConnection>();
        }

        public void Add(WebSocketConnection socket)
        {
            _allSockets.Add(socket);
        }

        public void Remove(WebSocketConnection socket)
        {
            _allSockets.Remove(socket);
        }

        public void Publish(object obj)
        {
            var serializer = new JavaScriptSerializer();
            var sJson = serializer.Serialize(obj);
            foreach (var socket in _allSockets.ToList())
            {
                socket.Send(sJson);
            }
        }
    }
}