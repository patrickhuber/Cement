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
    public class FileSystemSendAdapter : FileSystemAdapterBase, ISendAdapter, IAsyncSendAdapter
    {
        public FileSystemSendAdapter(IAdapterContext adapterContext, IFileSystem fileSystem)
            : base(adapterContext, fileSystem)
        { }

        public void Send(Messages.IMessage message)
        {
            Uri uri = GetChannelUri();
            using (var outputStream = fileSystem.OpenWrite(uri.LocalPath))
            {
                message.Body.CopyTo(outputStream);
            }
        }

        public async Task SendAsync(IMessage message)
        {
            Uri uri = GetChannelUri();
            using (var outputStream = fileSystem.OpenWrite(uri.LocalPath))
            {
                await message.Body.CopyToAsync(outputStream);
            }
        }

        public IChannel InChannel { get; private set; }
    }
}
