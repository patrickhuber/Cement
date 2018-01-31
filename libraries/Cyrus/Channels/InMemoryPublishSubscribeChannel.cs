using Cyrus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cyrus.Channels
{
    public class InMemoryPublishSubscribeChannel : ISendChannel, ISubscribableChannel
    {
        private List<IMessageHandler> _observers;

        public InMemoryPublishSubscribeChannel()
        {
            _observers = new List<IMessageHandler>();
        }

        public void Send(IMessageReader reader)
        {
            var streams = CreateStreams();

            using (reader)
            {
                Message.CopyTo(reader, streams);
                for (var i = 0; i < streams.Count; i++)
                {
                    var message = CreateMessage(reader, streams[i]);                    
                    _observers[i].Handle(message);
                }
            }
        }

        public async Task SendAsync(IMessageReader reader)
        {
            var streams = CreateStreams();

            using (reader)
            {
                await Message.CopyToAsync(reader, streams);
                for (var i = 0; i < _observers.Count; i++)
                {
                    var message = CreateMessage(reader, streams[i]);
                    await _observers[i].HandleAsync(message);
                }
            }
        }

        private List<MemoryStream> CreateStreams()
        {
            var streams = new List<MemoryStream>(_observers.Count);
            for (var i = 0; i < _observers.Count; i++)
                streams.Add(new MemoryStream());
            return streams;
        }

        private static Message CreateMessage(IMessageReader reader, MemoryStream body)
        {
            // create message properties from the existing message and 
            // assign new message id
            var properties = new Dictionary<string, string>(reader.MessageHeader);
            properties[MessageProperties.Id] = Guid.NewGuid().ToString();

            return new Message(body, properties);
        }

        public bool Subscribe(IMessageHandler handler)
        {
            if (_observers.Contains(handler))
                return false;
            
            _observers.Add(handler);
            return true;            
        }

        public bool Unsubscribe(IMessageHandler handler)
        {
            return _observers.Remove(handler);
        }

        bool IChannel.Send(IMessageReader reader)
        {
            throw new NotImplementedException();
        }

        public bool Send(IMessageReader reader, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
    }
}
