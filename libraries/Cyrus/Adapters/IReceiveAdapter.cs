using Cyrus.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Adapters
{
    /// <summary>
    /// Receives a message from the source and writes it to a channel.
    /// </summary>
    public interface IReceiveAdapter
    {
        ISendChannel SendChannel { get; }
        Task ReceiveAsync();
    }
}
