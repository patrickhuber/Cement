using Cyrus.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Reads a message from the channel and writes it to the sink
    /// </summary>
    public interface ISendAdapter
    {
        IReceiveChannel ReceiveChannel { get; }
        Task SendAsync();
    }
}
