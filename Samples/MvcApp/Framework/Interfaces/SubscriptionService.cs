using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;

namespace MvcApp.Framework
{
    public class SubscriptionService : ISubscriptionService
    {
        public IEnumerable<IListener<T>> GetSubscriptions<T>()
        {
            var consumers = ServiceLocator.Current.GetAllInstances(typeof(IListener<T>));
            return (IEnumerable<IListener<T>>)consumers;
        }
    }
}