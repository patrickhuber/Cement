using Cement.Channels;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Filters
{
    public interface IFilter : IMessageHandler
    {
        IChannel OutChannel { get; }
    }
}
