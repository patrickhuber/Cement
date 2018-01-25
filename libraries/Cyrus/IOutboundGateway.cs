using System.Threading.Tasks;

namespace Cyrus
{
    public interface IOutboundGateway 
        : IOutboundAdapter
    {
        ISendChannel OutboundChannel { get; }
    }
}
