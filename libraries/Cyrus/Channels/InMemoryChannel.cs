using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cyrus.Channels
{
    /// <summary>
    /// Represents an in memory message channel
    /// </summary>
    public class InMemoryChannel : IReceiveChannel, ISendChannel
    {
        private Queue<Message> _queue;

        public InMemoryChannel()
        {
            _queue = new Queue<Message>();
        }

        public int Count { get { return _queue.Count; } }

        public IMessageReader Receive()
        {
            if(Count > 0)
                return _queue.Dequeue();
            throw new InvalidOperationException("Queue is empty");
        }
        
        public void Send(IMessageReader reader)
        {
            using (reader)
            {
                // copy the message body to a MemoryStream
                var body = CopyMessageBodyToMemoryStream(reader);

                // copy the message headers
                var properties = new Dictionary<string, string>(reader.MessageHeader);

                // put the message in the message queue
                var newMessage = new Message(body, properties);

                _queue.Enqueue(newMessage);
            }
        }

        public async Task SendAsync(IMessageReader reader)
        {
            using (reader)
            {
                // copy the message body to a MemoryStream
                var body = await CopyMessageBodyToMemoryStreamAsync(reader);

                // copy the message headers
                var properties = new Dictionary<string, string>(reader.MessageHeader);

                // put the message in the message queue
                var newMessage = new Message(body, properties);

                _queue.Enqueue(newMessage);
            }
        }

        private static MemoryStream CopyMessageBodyToMemoryStream(IMessageReader reader)
        {
            var memoryStream = new MemoryStream();
            Message.CopyTo(reader, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        private static async Task<MemoryStream> CopyMessageBodyToMemoryStreamAsync(IMessageReader reader)
        {
            var memoryStream = new MemoryStream();
            await Message.CopyToAsync(reader, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}
