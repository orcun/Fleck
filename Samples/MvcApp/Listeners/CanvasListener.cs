using MvcApp.Framework;
using MvcApp.Messages;
using MvcApp.Models;

namespace MvcApp.Listeners
{
    public class CanvasListener : IListener<CanvasMessage>
    {
        private readonly ISocketManager _manager;

        public CanvasListener(ISocketManager manager)
        {
            _manager = manager;
        }

        public void Handle(CanvasMessage message)
        {
            _manager.Publish(new Canvas
            {
                X = message.X,
                Y = message.Y,
                Name = message.Name,
                R = message.R,
                G = message.G,
                B = message.B
            });
        }
    }
}