using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    /// <summary>
    /// Receives a file from the given location.
    /// </summary>
    public class FileReceiveAdapter : IReceiveAdapter
    {
        public IChannel OutputChannel { get; private set; }

        private IFileSystem _fileSystem;
        private FileReceiveAdapterSettings _settings;

        public FileReceiveAdapter(
            IChannel outputChannel,
            IFileSystem fileSystem, 
            FileReceiveAdapterSettings settings)
        {
            OutputChannel = outputChannel;
            _settings = settings;
            _fileSystem = fileSystem;
        }
        
        /// <summary>
        /// Receives the next file from the directory, creates a message and writes that message to the output channel.
        /// </summary>
        public void Receive()
        {
            var file = GetFirstFile();
            if (file == null)
                return;
            var header = CreateHeader(file);
            using (var fileStream = _fileSystem.OpenFile(file, FileMode.Open))
            {
                var message = new Message(fileStream, header);
                OutputChannel.Send(message);
            }
        }

        
        /// <summary>
        /// Receives the next file from the directory, creates a message and writes that message to the output channel.
        /// </summary>
        public async Task ReceiveAsync()
        {
            var file = GetFirstFile();
            if (file == null)
                return;
            var header = CreateHeader(file);
            using (var fileStream = _fileSystem.OpenFile(file, FileMode.Open))
            {
                var message = new Message(fileStream, header);
                await OutputChannel.SendAsync(message);
            }
        }

        private string GetFirstFile()
        {
            foreach (var file in _fileSystem.EnumerateFiles(_settings.Path, _settings.Filter))
            {
                return file;
            }
            return null;
        }

        private Dictionary<string, string> CreateHeader(string file)
        {
            return new Dictionary<string, string>
            {
                { MessageProperties.Id, Guid.NewGuid().ToString() },
                { FileReceiveMessageProperties.Path, file },
                { FileReceiveMessageProperties.Directory, _settings.Path },
                { FileReceiveMessageProperties.Filter, _settings.Filter },
                { FileReceiveMessageProperties.CreatedUtc,  _fileSystem.GetFileCreationTimeUtc(file).ToString() },
                { FileReceiveMessageProperties.LastWriteTimeUtc, _fileSystem.GetFileLastWriteTimeUtc(file).ToString() }
            };
        }
    }
}
