using System;
using System.Linq;
using System.Reflection;

namespace MvcApp.Framework
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly MethodInfo _publish;

        public EventAggregator(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            _publish = typeof(EventAggregator).GetMethod("Publish");
        }

        public void Publish<T>(T eventMessage)
        {
            var subscriptions = _subscriptionService.GetSubscriptions<T>();
            subscriptions.ToList().ForEach(x => PublishToConsumer(x, eventMessage));
        }

        private static void PublishToConsumer<T>(IListener<T> x, T eventMessage)
        {
            try
            {
                x.Handle(eventMessage);
            }
            finally
            {
                var instance = x as IDisposable;
                if (instance != null)
                {
                    instance.Dispose();
                }
            }
        }

        public void PublishMessage(object obj)
        {
            _publish.MakeGenericMethod(obj.GetType()).Invoke(this, new[] { obj });
        }
    }
}