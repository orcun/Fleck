using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;

namespace MvcApp.Framework.BootstrapperTasks
{
    public class WebsocketServerStartupTask : IBootstrapperTask
    {
        private readonly IWindsorContainer _container;

        public WebsocketServerStartupTask(IWindsorContainer container)
        {
            _container = container;
        }

        public int Order
        {
            get { return 1; }
        }

        public void Execute()
        {
			_container.Register(Component.For<ISocketManager>().ImplementedBy<SocketManager>().LifeStyle.Singleton);
            var socketManager = ServiceLocator.Current.GetInstance<ISocketManager>();
            new Server(socketManager);
        }
    }
}