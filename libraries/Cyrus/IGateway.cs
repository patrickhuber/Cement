using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    public interface IGateway
    {
        IReceiveChannel ReceiveChannel { get; }
        ISendChannel SendChannel { get; }
        Task SendAsync();
    }
}
