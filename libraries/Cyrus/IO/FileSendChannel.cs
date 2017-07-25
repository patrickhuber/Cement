using Cyrus.Channels;
using System;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileSendChannel : ISendChannel
    {
        public FileSystem FileSystem { get; private set; }

        public FileSendChannel(FileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IMessageWriter Send()
        {
            throw new NotImplementedException();
        }

        public void Send(IMessageReader reader)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(IMessageReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
