using System;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileSendAdapter : ISendAdapter
    {
        public IChannel InputChannel { get; private set; }
        private IFileSystem _fileSystem;
        private FileSendAdapterSettings _settings;

        public FileSendAdapter(
            IChannel inputChannel, 
            IFileSystem fileSystem,
            FileSendAdapterSettings settings)
        {
            _settings = settings;
            _fileSystem = fileSystem;
        }

        public void Send()
        {
            throw new NotImplementedException();
        }

        public Task SendAsync()
        {
            throw new NotImplementedException();
        }
    }
}
