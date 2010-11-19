using System.Web;
using MvcApp.Framework;
using MvcApp.Messages;
using MvcApp.Models;

namespace MvcApp.Listeners
{
    public class PollListener : IListener<SelectOptionMessage>
    {
        private readonly ISocketManager _manager;

        public PollListener(ISocketManager manager)
        {
            _manager = manager;
        }

        public void Handle(SelectOptionMessage message)
        {
            if (HttpRuntime.Cache["Poll"] == null)
            {
                HttpRuntime.Cache["Poll"] = new Poll();
            }
            var poll = (Poll)HttpRuntime.Cache["Poll"];

            if (message.Yes)
            {
                poll.Yes += 1;
            }
            else
            {
                poll.No += 1;
            }
            _manager.Publish(poll);
        }
    }
}