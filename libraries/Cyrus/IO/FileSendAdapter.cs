using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cyrus.Channels;
using System.IO;
using Cyrus.Messages;

namespace Cyrus.IO
{
    public class FileSendAdapter : ISendAdapter
    {
        public IReceiveChannel ReceiveChannel { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public string Path { get; private set; }

        public FileSendAdapter(IFileSystem fileSystem, string path, IReceiveChannel receiveChannel)
        {
            FileSystem = fileSystem;
            Path = path;
            ReceiveChannel = receiveChannel;
        }

        public async Task SendAsync()
        {
            var fileName = Guid.NewGuid().ToString() + ".txt";
            var path = System.IO.Path.Combine(Path, fileName);

            using (var message = ReceiveChannel.Receive())
            {               
                using (var fileStream = FileSystem.CreateFile(path))
                {
                    await Message.CopyToAsync(message, fileStream);
                }
            }
        }
    }
}
