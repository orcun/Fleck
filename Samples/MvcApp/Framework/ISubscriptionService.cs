using System.Collections.Generic;

namespace MvcApp.Framework
{
    public interface ISubscriptionService
    {
        IEnumerable<IListener<T>> GetSubscriptions<T>();
    }
}