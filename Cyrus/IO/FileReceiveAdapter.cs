using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileReceiveAdapter : IAdapter
    {
        public IReceiveChannel ReceiveChannel { get; private set; }

        public ISendChannel SendChannel { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public FileReceiveAdapter(IFileSystem fileSystem, ISendChannel sendChannel)
        {
            ReceiveChannel = new FileReceiveChannel();
        }
    }
}
