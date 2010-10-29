namespace MvcApp.Framework
{
    public interface IBootstrapperTask
    {
        int Order { get; }
        void Execute();
    }
}