using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cement.Channels
{
    class FileSystemChannel : IAsyncInputChannel, IAsyncOutputChannel, IInputChannel, IOutputChannel
    {
        public System.Threading.Tasks.Task ReceiveAsync(Messages.IMessage message)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<Messages.IMessage> SendAsync()
        {
            throw new NotImplementedException();
        }

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
