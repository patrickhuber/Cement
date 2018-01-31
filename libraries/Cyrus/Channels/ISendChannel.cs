using Cyrus.Messaging;
using System.Threading.Tasks;

namespace Cyrus.Channels
{
    /// <summary>
    /// Defines a channel specificly for sending messages.
    /// </summary>
    public interface ISendChannel
    {        
        /// <summary>
        /// The Send method receives a <see cref="IMessageReader">Message Source</see> from which this method reads.
        /// </summary>
        /// <param name="reader">The message sink from which the send method will read.</param>
        void Send(IMessageReader reader);

        /// <summary>
        /// The SendAsync method receives a <see cref="IMessageReader">Message Source </see> from which this method reads.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>An awaitable task.</returns>
        Task SendAsync(IMessageReader reader);
    }
}
