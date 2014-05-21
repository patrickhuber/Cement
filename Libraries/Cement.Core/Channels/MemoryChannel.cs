using Cement.Adapters;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Channels
{
    public class MemoryChannel : IInputChannel, IOutputChannel
    {
        private IAdapterContext channelContext;
        private Queue<IMessage> messageQueue;
        private IMessageFactory messageFactory;

        public MemoryChannel(IAdapterContext channelContext, Queue<IMessage> messageQueue, IMessageFactory messageFactory)
        {
            this.channelContext = channelContext;
            this.messageQueue = messageQueue;
            this.messageFactory = messageFactory;
        }

        public void Send(Messages.IMessage message)
        {
            var newMessage = messageFactory.Create();
            message.CopyTo(newMessage);
            this.messageQueue.Enqueue(message);
        }

        public string Protocol
        {
            get { return "memory"; }
        }

        public Messages.IMessage Receive()
        {
            return this.messageQueue.Dequeue();
        }
    }
}
