using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Defines an interface for receiving messages using push and pull semantics.
    /// </summary>
    public interface IReceiveChannel
    {
        IMessageSource Receive();
        Task<IMessageSource> ReceiveAsync();
        void Receive(IMessageSource source);
        Task ReceiveAsync(IMessageSource source);
    }
}
