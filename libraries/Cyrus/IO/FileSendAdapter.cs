using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cyrus.Channels;

namespace Cyrus.IO
{
    public class FileSendAdapter : ISendAdapter
    {
        public IReceiveChannel ReceiveChannel { get; private set; }

        public FileSendAdapter(IReceiveChannel receiveChannel)
        {
            ReceiveChannel = receiveChannel;
        }

        public Task SendAsync()
        {
            throw new NotImplementedException();
        }
    }
}
