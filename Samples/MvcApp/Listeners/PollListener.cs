using System.Web;
using MvcApp.Framework;
using MvcApp.Messages;
using MvcApp.Models;

namespace MvcApp.Listeners
{
    public class PollListener : IListener<SelectOptionMessage>
    {
        public void Handle(SelectOptionMessage message)
        {
            if (HttpContext.Current.Session["Poll"] == null)
            {
                HttpContext.Current.Session.Add("Poll", new Poll());
            }
            var poll = (Poll)HttpContext.Current.Session["Poll"];

            if (message.Yes)
            {
                poll.Yes += 1;
            }
            else
            {
                poll.No += 1;
            }
        }
    }
}