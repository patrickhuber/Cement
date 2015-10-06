using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Defines an interface for receiving messages using push and pull semantics.
    /// </summary>
    public interface IReceiveChannel
    {
        /// <summary>
        /// The Receive method will return a message source for the caller to read from and close.
        /// </summary>
        /// <returns>A message source from which to read.</returns>
        IMessageReader Receive();
                
        /// <summary>
        /// The Receive method receives a <see cref="IMessageWriter">Message Sink</see> to which this method writes. 
        /// </summary>
        /// <param name="writer">The message sink to which this method will write.</param>
        void Receive(IMessageWriter writer);

        /// <summary>
        /// The Receive method receives a <see cref="IMessageWriter">Message Sink </see> to which this method writes asynchronously.
        /// </summary>
        /// <param name="writer">The message sink to which this method writes.</param>
        /// <returns>The task representing the asynchronous work this method performs.</returns>
        Task ReceiveAsync(IMessageWriter writer);
    }
}
