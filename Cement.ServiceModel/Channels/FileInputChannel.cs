using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public class FileInputChannel : FileChannelBase, IInputChannel
    {
        public FileInputChannel(BufferManager bufferManager, MessageEncoderFactory encoderFactory, ChannelManagerBase channelManager)
            : base(bufferManager, encoderFactory, channelManager)
        { }
        
        public EndpointAddress LocalAddress
        {
            get;
            private set;
        }

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

        public Message Receive(TimeSpan timeout)
        {
            var message = ReadMessage(LocalAddress);
            return message;
        }

        private Message ReadMessage(EndpointAddress localAddress)
        {
            return ReadMessage(localAddress.Uri);
        }

        private Message ReadMessage(Uri uri)
        {
            PathAndPattern pathAndPattern = ParseFileUri(uri);
            var file = Directory.EnumerateFiles(pathAndPattern.Path, pathAndPattern.Pattern).FirstOrDefault();
            if (file == null)
                throw new IOException(
                    string.Format("Unable to read file from path {0}", uri));
            
        }

        public Message Receive()
        {
            return Receive(DefaultReceiveTimeout);
        }

        public bool TryReceive(TimeSpan timeout, out Message message)
        {
            message = null;
            bool complete = WaitForMessage(timeout);
            if (!complete)
                return false;
            message = Receive(timeout);
            return true;
        }
                
        public bool WaitForMessage(TimeSpan timeout)
        {
            var localAddress = LocalAddress;
            var pathAndPattern = ParseFileUri(localAddress.Uri);

            var poller = new FileSystemPoller(pathAndPattern.Path, pathAndPattern.Pattern);
            poller.IncludeSubDirectories = false;
            var result = poller.WaitForFile(timeout);
            
            return !result.TimedOut;
        }        
    }
}
