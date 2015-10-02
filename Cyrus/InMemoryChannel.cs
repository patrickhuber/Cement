using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Represents an in memory channel that will read a message input message body
    /// </summary>
    public class InMemoryChannel : IChannel
    {
        private Queue<IMessage> _messageQueue;

        public InMemoryChannel()
        {
            _messageQueue = new Queue<IMessage>();
        }

        /// <summary>
        /// Receive a message. 
        /// </summary>
        /// <returns>The message object from the queue if one exists. Null otherwise.</returns>
        public IMessage Receive()
        {
            // The message in the channel will already have been read from the source and stored in memory, so no buffer copying is performed.
            if (_messageQueue.Count == 0)
                return null;

            return _messageQueue.Dequeue();
        }

        /// <summary>
        /// Receive a message from the channel. 
        /// </summary>
        /// <returns></returns>
        public async Task<IMessage> ReceiveAsync()
        {
            // The message in the channel will already have been read from the source and stored in memory, so no buffer copying is performed.
            return await Task.FromResult(Receive());
        }

        /// <summary>
        /// Send the message to the channel.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public void Send(IMessage message)
        {
            AssertMessageIsNotNull(message);

            // create a inner message to actually store
            var innerMessage = new Message(
                new MemoryStream(), 
                new Dictionary<string, string>(message.Header));

            var buffer = new byte[2048];
            int bytesRead = 0;
            do
            {
                bytesRead = message.Body.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    innerMessage.Body.Write(buffer, 0, bytesRead);
                }
            } while (bytesRead > 0);
            
            _messageQueue.Enqueue(innerMessage);
        }

        /// <summary>
        /// Send the message to the channel.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task represting the execution correlation.</returns>
        public async Task SendAsync(IMessage message)
        {
            AssertMessageIsNotNull(message);

            // create a inner message to actually store
            var innerMessage = new Message(
                new MemoryStream(),
                new Dictionary<string, string>(message.Header));

            var buffer = new byte[2024];
            int bytesRead = 0;
            do
            {
                bytesRead = await message.Body.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    await innerMessage.Body.WriteAsync(buffer, 0, bytesRead);
                }

            } while (bytesRead > 0);

            _messageQueue.Enqueue(innerMessage);
        }

        private static void AssertMessageIsNotNull(IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message", "The message argument is null.");
        }
    }
}
