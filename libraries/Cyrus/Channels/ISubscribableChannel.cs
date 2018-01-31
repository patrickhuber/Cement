using Cyrus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Channels
{
    public interface ISubscribableChannel : IChannel
    {
        bool Subscribe(IMessageHandler handler);
        bool Unsubscribe(IMessageHandler handler);
    }
}
