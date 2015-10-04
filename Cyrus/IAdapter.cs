using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    public interface IAdapter
    {
        ISendChannel SendChannel { get; }

        IReceiveChannel ReceiveChannel { get; }
    }
}
