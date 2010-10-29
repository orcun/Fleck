using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Facilities.FactorySupport;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Releasers;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Microsoft.Practices.ServiceLocation;

namespace MvcApp.Framework
{
    public class Bootstrapper
    {
        private IWindsorContainer _container;

        public Bootstrapper()
        {
            ConfigureContainer();
        }

        public void Run()
        {
            var tasks = _container.ResolveAll<IBootstrapperTask>().OrderBy(x => x.Order);
            foreach (var task in tasks)
            {
                task.Execute();
            }
        }

        private void ConfigureContainer()
        {
            _container = new WindsorContainer(new XmlInterpreter());
            _container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();
            _container.AddFacility("factory.support", new FactorySupportFacility());
            _container.Register(
                Component.For<RouteCollection>()
                .Instance(RouteTable.Routes),
                Component.For<ModelBinderDictionary>()
                .Instance(ModelBinders.Binders)
            );
            _container.Register(
                AllTypes.Of<IBootstrapperTask>().FromAssembly(GetType().Assembly)
            );
            _container.Kernel.AddComponentInstance(typeof(IWindsorContainer).FullName, typeof(IWindsorContainer),
                                                   _container);
            var serviceLocatorAdapter = new WindsorServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => serviceLocatorAdapter);
            _container.Kernel.AddComponentInstance<IServiceLocator>(serviceLocatorAdapter);
        }
    }
}