using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// A Transmitter takes information from a channel and transmits that information to some source.
    /// </summary>
    public interface ISendAdapter
    {
        IChannel InputChannel { get; }

        void Send();

        Task SendAsync();
    }
}
