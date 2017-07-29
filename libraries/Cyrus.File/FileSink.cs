using System;
using System.Threading.Tasks;

namespace Cyrus.File
{
    public class FileSink : ISink
    {
        public IReceiveChannel ReceiveChannel { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public string Path { get; private set; }

        public FileSink(IFileSystem fileSystem, string path, IReceiveChannel receiveChannel)
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
