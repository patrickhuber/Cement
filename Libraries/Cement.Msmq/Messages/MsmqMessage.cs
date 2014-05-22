using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Msmq.Messages
{
    public class MsmqMessage : IMessage
    {
        private MsmqStream _bodyStream;
        private System.Messaging.Message _currentMessage;
        private System.Messaging.MessageQueue _messageQueue;

        public MsmqMessage()
        {
            _currentMessage = new System.Messaging.Message();
            _bodyStream = new MsmqStream(_currentMessage, _messageQueue);
        }

        public System.IO.Stream Body
        {
            get { throw new NotImplementedException(); }
        }

        public IMessageContext Context
        {
            get { throw new NotImplementedException(); }
        }

        public void CopyTo(IMessage message)
        {
            
        }

        public Task CopyToAsync(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
