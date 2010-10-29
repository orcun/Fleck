using System;

namespace MvcApp.Framework
{
    public class SubscribeAttribute : Attribute
    {
        private readonly string _uri;

        public SubscribeAttribute(string uri)
        {
            _uri = uri;
        }

        public string Uri
        {
            get { return _uri; }
        }
    }
}