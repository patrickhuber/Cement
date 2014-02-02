using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Referee.ServiceModel.Channels
{
    public class FileInputChannel : FileChannelBase, IInputChannel
    {
        public FileInputChannel(ChannelManagerBase channelManager)
            : base(channelManager)
        { }

        public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public Message EndReceive(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool EndTryReceive(IAsyncResult result, out Message message)
        {
            throw new NotImplementedException();
        }

        public bool EndWaitForMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public System.ServiceModel.EndpointAddress LocalAddress
        {
            get { throw new NotImplementedException(); }
        }

        public Message Receive(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public Message Receive()
        {
            throw new NotImplementedException();
        }

        public bool TryReceive(TimeSpan timeout, out Message message)
        {
            throw new NotImplementedException();
        }

        public bool WaitForMessage(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
    }
}
