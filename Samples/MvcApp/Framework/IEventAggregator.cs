namespace MvcApp.Framework
{
    public interface IEventAggregator
    {
        void Publish<T>(T eventMessage);
        void PublishMessage(object obj);
    }
}