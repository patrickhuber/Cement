using Cyrus.Messaging;
using System;

namespace Cyrus.Channels
{
    public interface IChannel
    {
        bool Send(IMessageReader reader);
        bool Send(IMessageReader reader, TimeSpan timeout);
    }
}