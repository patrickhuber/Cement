using Cement.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public class FileOutputChannel : FileChannelBase, IOutputChannel
    {
        public FileOutputChannel(
            BufferManager bufferManager, 
            MessageEncoderFactory encoderFactory, 
            ChannelManagerBase channelManager,
            IFileSystem fileSystem)
            : base(bufferManager, encoderFactory, channelManager, fileSystem)
        { }

        public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public void EndSend(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public System.ServiceModel.EndpointAddress RemoteAddress
        {
            get { throw new NotImplementedException(); }
        }

        public void Send(Message message, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void Send(Message message)
        {
            throw new NotImplementedException();
        }

        public Uri Via
        {
            get { throw new NotImplementedException(); }
        }
    }
}
