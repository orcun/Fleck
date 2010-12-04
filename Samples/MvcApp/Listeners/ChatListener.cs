using System.Web;
using MvcApp.Framework;
using MvcApp.Messages;
using MvcApp.Models;

namespace MvcApp.Listeners
{
    public class ChatListener : IListener<ChatMessage>
    {
        private readonly ISocketManager _manager;

        public ChatListener(ISocketManager manager)
        {
            _manager = manager;
        }

        public void Handle(ChatMessage message)
        {
            _manager.Publish(new Chat { Text = HttpUtility.HtmlEncode(message.Text) });
        }
    }
}