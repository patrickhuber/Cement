using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Channels
{
    public interface IAsyncOutputChannel : IChannel
    {
        Task SendAsync(IMessage message);
    }
}
