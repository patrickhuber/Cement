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
        /// Returns the current count of messages
        /// </summary>
        int Count { get; }
    }
}
