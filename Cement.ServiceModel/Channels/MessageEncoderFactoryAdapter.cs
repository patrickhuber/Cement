using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Cement.ServiceModel.Channels
{
    public class MessageEncoderFactoryAdapter : IMessageEncoderFactory
    {
        private MessageEncoderFactory messageEncoderFactory;

        public MessageEncoderFactoryAdapter(MessageEncoderFactory messageEncoderFactory)
        {
            this.messageEncoderFactory = messageEncoderFactory;
        }

        public IMessageEncoder CreateSessionEncoder()
        {
            return new MessageEncoderAdapter(messageEncoderFactory.CreateSessionEncoder());
        }
    }
}
