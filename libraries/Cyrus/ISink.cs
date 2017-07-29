using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Reads a message from the channel and writes it to the sink
    /// </summary>
    public interface ISink
    {
        IReceiveChannel ReceiveChannel { get; }
        Task SendAsync();
    }
}
