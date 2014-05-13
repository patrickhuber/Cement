using Cement.IO;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Adapters
{

    public class FileSystemReceiveAdapter : FileSystemChannelBase, IReceiveAdapter
    {
        public FileSystemReceiveAdapter(IAdapterContext channelContext, IFileSystem fileSystem)
            : base(channelContext, fileSystem)
        { 
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
    }
}
