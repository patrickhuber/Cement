using Cyrus.Messaging;
using System;
using System.Collections.Generic;

namespace Cyrus
{
    /// <summary>
    /// <see cref="https://msdn.microsoft.com/en-us/library/dd990377(v=vs.110).aspx"/>
    /// </summary>
    class Unsubscriber : IDisposable
    {
        private IList<IMessageHandler> _handlers;
        private IMessageHandler _handler;

        public Unsubscriber(IList<IMessageHandler> handlers, IMessageHandler handler)
        {
            _handlers = handlers;
            _handler = handler;
        }

        public void Dispose()
        {
            if (_handler != null && _handlers.Contains(_handler))
                _handlers.Remove(_handler);
        }
    }
 
}
