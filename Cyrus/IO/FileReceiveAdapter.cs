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

        public FileReceiveAdapter(IFileSystem fileSystem, string path, string filter, ISendChannel sendChannel)
        {
            ReceiveChannel = new FileReceiveChannel(fileSystem, path, filter);
        }
    }
}
