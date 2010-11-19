using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace MvcApp.Framework.BootstrapperTasks
{
    public class StartEventAggregatorTask : IBootstrapperTask
    {
        private readonly IWindsorContainer _container;

        public StartEventAggregatorTask(IWindsorContainer container)
        {
            _container = container;
        }

        public int Order
        {
            get { return 0; }
        }

        public void Execute()
        {
			_container.Register(Component.For<ISubscriptionService>().ImplementedBy<SubscriptionService>().LifeStyle.Singleton);
			_container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifeStyle.Singleton);
        }
    }
}