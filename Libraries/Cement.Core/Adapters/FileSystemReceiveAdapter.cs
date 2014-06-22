using Cement.Channels;
using Cement.IO;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Adapters
{

    public class FileSystemReceiveAdapter : FileSystemAdapterBase, IReceiveAdapter
    {
        public FileSystemReceiveAdapter(IAdapterContext channelContext, IFileSystem fileSystem, IChannel messageChannel)
            : base(channelContext, fileSystem)
        {
            OutChannel = messageChannel;
        }

        public void Receive()
        {
            Uri uri = GetChannelUri();

            IMessageContext messageContext = new MessageContext();
            messageContext.Attributes.Add(MessageProperties.DateTimeUtc, DateTime.UtcNow.ToString());
            messageContext.Attributes.Add(MessageProperties.Id, Guid.NewGuid().ToString());
            messageContext.Attributes.Add(MessageProperties.ReceiveUri, uri.ToString());

            var message = new Message(fileSystem.OpenRead(uri.LocalPath), messageContext);
            OutChannel.Publish(message);
        }

        public IChannel OutChannel { get; private set; }
    }
}
