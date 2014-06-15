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
        IMessageFactory messageFactory;

        public FileSystemReceiveAdapter(IAdapterContext channelContext, IFileSystem fileSystem, IMessageFactory messageFactory)
            : base(channelContext, fileSystem)
        {
            this.messageFactory = messageFactory;
        }

        public Messages.IMessage Receive()
        {
            Uri uri = GetChannelUri();

            var message = messageFactory.Create();
            using (var stream = fileSystem.OpenRead(uri.LocalPath))
            {
                stream.CopyTo(message.Body);
            }
                        
            message.Context.Attributes.Add(MessageProperties.DateTimeUtc, DateTime.UtcNow.ToString());
            message.Context.Attributes.Add(MessageProperties.Id, Guid.NewGuid().ToString());
            message.Context.Attributes.Add(MessageProperties.ReceiveUri, uri.ToString());
                        
            return message;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IMessage value)
        {
            throw new NotImplementedException();
        }
    }
}
