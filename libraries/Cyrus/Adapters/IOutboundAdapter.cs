using Cyrus.Channels;
using System.Threading.Tasks;

namespace Cyrus.Adapters
{
    /// <summary>
    /// Reads a message from the channel and writes it to the sink
    /// </summary>
    public interface IOutboundAdapter
    {
        IReceiveChannel InboundChannel { get; }
        Task SendAsync();
    }
}
