using System;
using Cyrus.Adapters;
using Cyrus.Channels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileReceiveAdapter : IReceiveAdapter
    {
        public ISendChannel SendChannel { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public string Path { get; private set; }

        public string Filter { get; private set; }


        public FileReceiveAdapter(
            IFileSystem fileSystem, 
            string path, 
            string filter,
            ISendChannel sendChannel)
        {
            FileSystem = fileSystem;
            Path = path;
            Filter = filter;
            SendChannel = sendChannel;
        }

        private IDictionary<string, string> GetMessageHeaders(string file)
        {
            return new Dictionary<string, string>()
            {
                { FileReceiveProperties.Path, file },
                { FileReceiveProperties.Filter, Filter },
                { FileReceiveProperties.LastWriteTimeUtc, FileSystem.GetFileLastWriteTimeUtc(file).ToString() },
                { FileReceiveProperties.CreationTimeUtc, FileSystem.GetFileCreationTimeUtc(file).ToString() },
                { FileReceiveProperties.SourceFileName, FileSystem.GetFileName(file) }
            };
        }

        public async Task ReceiveAsync()
        {
            var files = FileSystem.EnumerateFiles(Path, Filter);
            foreach (var file in files)
            {
                await ReceiveFileAsync(file);
                await DeleteFileAsync(file);
            }
        }
        
        private async Task ReceiveFileAsync(string file)
        {
            using (var fileStream = FileSystem.OpenFile(file, System.IO.FileMode.Open))
            {
                var message = new Message(fileStream, GetMessageHeaders(file));
                await SendChannel.SendAsync(message);
            }
        }

        private async Task DeleteFileAsync(string file)
        {
            FileSystem.DeleteFile(file);
            await Task.FromResult(0);
        }
    }
}
