using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// A receiver takes information from a source and writes it to a channel
    /// </summary>
    public interface IReceiveAdapter
    {
        IChannel OutputChannel { get; }

        void Receive();
        Task ReceiveAsync();
    }
}
