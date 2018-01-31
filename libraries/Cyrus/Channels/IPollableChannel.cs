using Cyrus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrus.Channels
{
    public interface IPollableChannel : IChannel
    {
        IMessageReader Receive();
        IMessageReader Receive(TimeSpan timeout);
    }
}
