using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    public interface ISendChannel
    {
        IMessageSink Send();
        Task<IMessageSink> SendAsync();
        void Send(IMessageSink sink);
        Task SendAsync(IMessageSink sink);
    }
}
