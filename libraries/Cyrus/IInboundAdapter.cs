using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Receives a message from the source and writes it to a channel.
    /// </summary>
    public interface IInboundAdapter
    {
        ISendChannel OutboundChannel { get; }
        Task ReceiveAsync();
    }
}
