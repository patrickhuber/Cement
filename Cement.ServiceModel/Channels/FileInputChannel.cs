using Cement.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Cement.Threading.Tasks;
using System.Threading.Tasks;

namespace Cement.ServiceModel.Channels
{
    public class FileInputChannel : FileChannelBase, IInputChannel
    {
        private long maxReceivedMessageSize;
        private IChannelManager channelManager;

        public FileInputChannel(
            ChannelManagerBase channelManager, 
            IBufferManager bufferManager, 
            IMessageEncoderFactory encoderFactory,
            long maxReceivedMessageSize,
            IFileSystem directory)
            : base(channelManager, bufferManager, encoderFactory, directory)
        {
            Initialize(new ChannelManagerAdapter(channelManager), bufferManager, encoderFactory, maxReceivedMessageSize, directory);
        }

        public void Initialize(
            IChannelManager channelManager,
            IBufferManager bufferManager,
            IMessageEncoderFactory encoderFactory,
            long maxReceivedMessageSize,
            IFileSystem directory)
        {
            this.channelManager = channelManager;
            this.maxReceivedMessageSize = maxReceivedMessageSize;
        }

        public EndpointAddress LocalAddress
        {
            get;
            private set;
        }

        #region Receive 
        
        public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public Message EndReceive(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public Message Receive(TimeSpan timeout)
        {
            var message = ReadMessage(LocalAddress);
            return message;
        }

        public Message Receive()
        {
            return Receive(DefaultReceiveTimeout);
        }

        private Message ReadMessage(EndpointAddress localAddress)
        {
            return ReadMessage(localAddress.Uri);
        }

        private Message ReadMessage(Uri uri)
        {
            PathAndPattern pathAndPattern = ParseFileUri(uri);
            return ReadMessageFromFile(pathAndPattern);
        }

        private Message ReadMessageFromFile(PathAndPattern pathAndPattern)
        {
            var firstFile = fileSystem.GetFirstFile(
                pathAndPattern.Path,
                pathAndPattern.Pattern,
                false);
            using (Stream stream = fileSystem.OpenRead(firstFile))
            {
                return ReadMessageFromStream(stream);
            }
        }

        private Message ReadMessageFromStream(Stream stream)
        {
            var messageReader = new MessageReader(
                stream,
                bufferManager,
                messageEncoder,
                maxReceivedMessageSize);
            return messageReader.Read();
        }

        #endregion Receive

        #region TryReceive

        public Task<bool> TryReceiveAsync(TimeSpan timeout, out Message message)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Message message;
            return TryReceiveAsync(timeout, out message).AsApm(callback, state);
        }

        public bool EndTryReceive(IAsyncResult result, out Message message)
        {            
            throw new NotImplementedException();
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

        #endregion TryReceive

        #region WaitForMessage

        public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public bool EndWaitForMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool WaitForMessage(TimeSpan timeout)
        {
            var localAddress = LocalAddress;
            var pathAndPattern = ParseFileUri(localAddress.Uri);

            var poller = new FileSystemPoller(
                pathAndPattern.Path,
                pathAndPattern.Pattern,
                fileSystem);
            poller.IncludeSubDirectories = false;
            var result = poller.WaitForFile(timeout);

            return !result.TimedOut;
        }   

        #endregion WaitForMessage  
    }
}
