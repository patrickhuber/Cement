using Cyrus.Messaging;
using System;
using System.Threading.Tasks;

namespace Cyrus.Channels
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
        /// <exception cref="System.InvalidOperationException">Throws invalid operation exception if no messages are in the queue.</exception>
        Task<IMessageReader> ReceiveAsync();

        /// <summary>
        /// Blocks on receive, the blocking will expire after the timeout elapses.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        Task<IMessageReader> ReceiveAsync(TimeSpan timeSpan);
                
        /// <summary>
        /// Returns the current count of messages
        /// </summary>
        int Count { get; }
    }
}
