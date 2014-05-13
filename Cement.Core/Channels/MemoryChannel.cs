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

        public MemoryChannel(IAdapterContext channelContext, Queue<IMessage> messageQueue)
        {
            this.channelContext = channelContext;
            this.messageQueue = messageQueue;
        }

        public void Send(Messages.IMessage message)
        {
            var body = new MemoryStream();
            var context = new MessageContext();
            var memoryMessage = new Message
            {
                Body = body,
                Context = context
            };
            this.messageQueue.Enqueue(memoryMessage);
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
