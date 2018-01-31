using Cyrus.Adapters;
using Cyrus.Channels;

namespace Cyrus.Gateways
{
    public interface IInboundGateway : IInboundAdapter
    {
        IReceiveChannel InboundChannel { get; }
    }
}
