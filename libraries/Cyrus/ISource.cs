using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Receives a message from the source and writes it to a channel.
    /// </summary>
    public interface ISource
    {
        ISendChannel SendChannel { get; }
        Task ReceiveAsync();
    }
}
