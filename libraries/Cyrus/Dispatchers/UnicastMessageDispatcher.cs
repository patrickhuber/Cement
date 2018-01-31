using Cyrus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrus.Dispatchers
{
    class UnicastMessageDispatcher : IMessageDispatcher
    {
        public bool AddHandler(IMessageHandler handler)
        {
            throw new NotImplementedException();
        }

        public bool Dispatch(IMessageReader message)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHandler(IMessageHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
