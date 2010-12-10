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
    public class Server
    {
        public Server(ISocketManager socketManager)
        {
            var serializer = new JavaScriptSerializer();
            //Whats with origin?
            var server = new WebSocketServer("ws://localhost:8181");
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
                SocketSendAdapter socketAdapter = null;
                socket.OnOpen = () =>
                {
                    socketAdapter = new SocketSendAdapter(socket);
                    socketManager.Add(socket);
                };
                socket.OnClose = () =>
                {
                    socketManager.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    socketManager.MessageBegin(socket);
                    var deserialized = serializer.Deserialize<WireMessage>(message);
                    var socketMessage = (ISocketMessage)serializer.Deserialize(serializer.Serialize(deserialized.Data), socketMessages[deserialized.Uri]);
                    socketMessage.Socket = socketAdapter;
                    eventAggregator.PublishMessage(socketMessage);
                    socketManager.MessageEnd();
                };
            });
        }
    }
}