namespace MvcApp.Framework
{
    public interface IListener<T>
    {
        void Handle(T message);
    }
}