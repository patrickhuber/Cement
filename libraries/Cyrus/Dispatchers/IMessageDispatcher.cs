using Cyrus.Messaging;
using System.Collections.Generic;

namespace Cyrus.Dispatchers
{
    public interface IMessageDispatcher
    {
        bool AddHandler(IMessageHandler handler);
        bool RemoveHandler(IMessageHandler handler);
        bool Dispatch(IMessageReader message);
    }
}