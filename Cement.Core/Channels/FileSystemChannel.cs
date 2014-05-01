using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cement.Channels
{
    public class FileSystemChannel : IInputChannel, IOutputChannel
    {
        public void Receive(Messages.IMessage message)
        {
            throw new NotImplementedException();
        }

        public Messages.IMessage Send()
        {
            throw new NotImplementedException();
        }
    }
}
