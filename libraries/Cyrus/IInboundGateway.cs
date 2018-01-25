using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrus
{
    public interface IInboundGateway : IInboundAdapter
    {
        IReceiveChannel InboundChannel { get; }
    }
}
