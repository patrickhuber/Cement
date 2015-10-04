using System;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileReceiveChannel : IReceiveChannel
    {
        public IMessageSource Receive()
        {
            throw new NotImplementedException();
        }

        public void Receive(IMessageSource source)
        {
            throw new NotImplementedException();
        }

        public Task<IMessageSource> ReceiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(IMessageSource source)
        {
            throw new NotImplementedException();
        }
    }
}