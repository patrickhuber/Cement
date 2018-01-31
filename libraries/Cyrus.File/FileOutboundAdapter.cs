using Cyrus.Adapters;
using Cyrus.Channels;
using Cyrus.Messaging;
using System;
using System.Threading.Tasks;

namespace Cyrus.File
{
    public class FileOutboundAdapter
        : IOutboundAdapter
    {
        public IReceiveChannel InboundChannel { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public string Path { get; private set; }

        public FileOutboundAdapter(IFileSystem fileSystem, string path, IReceiveChannel receiveChannel)
        {
            FileSystem = fileSystem;
            Path = path;
            InboundChannel = receiveChannel;
        }

        public async Task SendAsync()
        {
            var fileName = Guid.NewGuid().ToString() + ".txt";
            var path = System.IO.Path.Combine(Path, fileName);

            using (var message = await InboundChannel.ReceiveAsync())
            {               
                using (var fileStream = FileSystem.CreateFile(path))
                {
                    await Message.CopyToAsync(message, fileStream);
                }
            }
        }
    }
}
