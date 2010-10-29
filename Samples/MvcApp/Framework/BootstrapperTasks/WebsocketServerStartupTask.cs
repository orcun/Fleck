namespace MvcApp.Framework.BootstrapperTasks
{
    public class WebsocketServerStartupTask : IBootstrapperTask
    {
        public int Order
        {
            get { return 0; }
        }

        public void Execute()
        {
            new Server();
        }
    }
}