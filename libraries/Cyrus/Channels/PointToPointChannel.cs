using Cyrus.Dispatchers;
using Cyrus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Channels
{
    public class PointToPointChannel : ISubscribableChannel, ISendChannel
    {
        readonly IMessageDispatcher _messageDispatcher;

        public PointToPointChannel(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
        }

        public void Send(IMessageReader reader)
        {
            throw new NotImplementedException();
        }

        public bool Send(IMessageReader reader, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(IMessageReader reader)
        {
            throw new NotImplementedException();
        }

        public bool Subscribe(IMessageHandler handler)
        {
            return false;
        }

        public bool Unsubscribe(IMessageHandler handler)
        {
            return true;
        }

        bool IChannel.Send(IMessageReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
