using Cement.IO;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cement.Channels
{
    public class FileSystemChannel : IInputChannel, IOutputChannel
    {
        private IChannelContext channelContext;
        private IFileSystem fileSystem;

        public FileSystemChannel(IChannelContext channelContext, IFileSystem fileSystem)
        {
            this.channelContext = channelContext;
            this.fileSystem = fileSystem;
        }

        public Messages.IMessage Receive()
        {            
            Uri uri = GetChannelUri();

            var message = new Message();
            message.Body = fileSystem.OpenRead(uri.LocalPath);

            var messageContext = new MessageContext();
            messageContext.Attributes.Add(MessageProperties.DateTimeUtc, DateTime.UtcNow.ToString());
            messageContext.Attributes.Add(MessageProperties.Id, Guid.NewGuid().ToString());
            messageContext.Attributes.Add(MessageProperties.ReceiveUri, uri.ToString());

            message.Context = messageContext;
            return message;
        }

        private Uri GetChannelUri()
        {
            string uriText;
            Uri uri;
            if (!channelContext.Attributes.TryGetValue(Cement.Channels.ChannelProperties.Uri, out uriText))
                throw new InvalidDataException("Unable to locate uri, the channel is misconfigured. Please specify a Uri in the Channel Settings.");
            uri = default(Uri);
            try
            {
                uri = new Uri(uriText);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Channel Uri is not in the correct format. Please format as a System.Uri object.", ex);
            }
            return uri;
        }

        public void Send(Messages.IMessage message)
        {
            Uri uri = GetChannelUri();
            using(var outputStream = fileSystem.OpenWrite(uri.LocalPath))
            {
                message.Body.CopyTo(outputStream);
            }
        }

        public string Protocol
        {
            get { return FileSystemProperties.Protocol; }
        }
    }
}
