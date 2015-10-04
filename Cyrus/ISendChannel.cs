using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Defines a channel specificly for sending messages.
    /// </summary>
    public interface ISendChannel
    {
        /// <summary>
        /// The Send method will return a message sink for the caller to populate and close.
        /// </summary>
        /// <returns></returns>
        IMessageSink Send();
        
        /// <summary>
        /// The Send method receives a <see cref="IMessageSource">Message Source</see> from which this method reads.
        /// </summary>
        /// <param name="sink">The message sink from which the send method will read.</param>
        void Send(IMessageSource sink);

        /// <summary>
        /// The SendAsync method receives a <see cref="IMessageSource">Message Source </see> from which this method reads.
        /// </summary>
        /// <param name="sink"></param>
        /// <returns>An awaitable task.</returns>
        Task SendAsync(IMessageSource sink);
    }
}
