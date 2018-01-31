using Cyrus.Adapters;
using Cyrus.Channels;
using System.Threading.Tasks;

namespace Cyrus.Gateways
{
    public interface IOutboundGateway 
        : IOutboundAdapter
    {
        ISendChannel OutboundChannel { get; }
    }
}
