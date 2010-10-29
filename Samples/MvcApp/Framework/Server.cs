using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Castle.Core;
using Fleck;
using Microsoft.Practices.ServiceLocation;

namespace MvcApp.Framework
{
    [Singleton]
    public class Server : IWebSocketServer
    {
        readonly List<WebSocketConnection> _allSockets;
        public Server()
        {
            var serializer = new JavaScriptSerializer();
            _allSockets = new List<WebSocketConnection>();
            //Whats with origin?
            var server = new WebSocketServer(8181, "http://localhost:54426", "ws://localhost:8181");
            var socketMessages = new Dictionary<string, Type>();
            foreach (var type in typeof (Server).Assembly.GetTypes().Where(t => typeof (ISocketMessage).IsAssignableFrom(t)))
            {
                var attribute = type.GetCustomAttributes(typeof (SubscribeAttribute), false).FirstOrDefault() as
                    SubscribeAttribute;
                if(attribute != null)
                {
                    socketMessages.Add(attribute.Uri, type);
                }
                
            }
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    _allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    _allSockets.Remove(socket);
                };
                //wire format
                //string Type
                //object Message
                //socket.Send
                //socket.OnMessage = message => Console.WriteLine(message);
                socket.OnMessage = message =>
                {
                    var deserialized = serializer.Deserialize<WireMessage>(message);
                    var socketAdapter = new SocketSendAdapter(socket);
                    var socketMessage = (ISocketMessage)serializer.Deserialize(deserialized.Data, socketMessages[deserialized.Uri]);
                    socketMessage.Socket = socketAdapter;
                    eventAggregator.PublishMessage(socketMessage);
                };
            });
        }

        public void Publish(object obj)
        {
            var serializer = new JavaScriptSerializer();
            string sJson = serializer.Serialize(obj);
            foreach (var socket in _allSockets.ToList())
            {
                socket.Send(sJson);
            }
        }
    }

    public class WireMessage
    {
        public string Uri { get; set; }
        public string Data { get; set; }
    }
}