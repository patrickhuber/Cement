using System;
using Cement.Messages;
using System.IO;

namespace Cement.Channels
{
    public interface IChannel
    {
        IMessage CreateMessage(IMessageContext messageContext);

        void Publish(IMessage message);
    }
}
