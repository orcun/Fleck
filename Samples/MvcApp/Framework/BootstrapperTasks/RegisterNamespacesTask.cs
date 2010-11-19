using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace MvcApp.Framework.BootstrapperTasks
{
    public class RegisterNamespacesTask : IBootstrapperTask
    {
        private readonly IWindsorContainer _container;

        public RegisterNamespacesTask(IWindsorContainer container)
        {
            _container = container;
        }

        public int Order
        {
            get { return 3; }
        }

        public void Execute()
        {
            _container.Register(
                AllTypes.FromAssembly(GetType().Assembly).Pick()
                    //.Unless(t => t.Namespace != null &&
                    //             (t.Namespace.Equals("WebApplication.Models.Domain")
                    //              || t.Namespace.Equals("WebApplication.ApplicationCode.Registration")
                    //              || t.Namespace.StartsWith("WebApplication.Validators")))
                    .WithService.FirstInterface()
                    .Configure(c => c.LifeStyle.Transient)
                );
        }
    }
}