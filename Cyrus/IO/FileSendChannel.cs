using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileSendChannel : ISendChannel
    {
        public IMessageSink Send()
        {
            throw new NotImplementedException();
        }

        public void Send(IMessageSource sink)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(IMessageSource sink)
        {
            throw new NotImplementedException();
        }
    }
}
