using Cyrus.Channels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cyrus
{
    public class NullSendChannel : ISendChannel
    {
        public IMessageWriter Send()
        {
            return new NullMessageWriter();
        }

        private class NullMessageWriter : IMessageWriter
        {
            public IDictionary<string, string> MessageHeader { get; private set;}

            public NullMessageWriter()
            {
                MessageHeader = new Dictionary<string, string>();
            }

            public void Close()
            {
            }

            public void Dispose()
            {
            }

            public void Write(byte[] buffer, int offset, int count)
            {
            }

            public Task WriteAsync(byte[] buffer, int offset, int count)
            {
                return Task.FromResult(0);
            }
        }

        public void Send(IMessageReader reader)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[1024];
            do
            {
                bytesRead = reader.Read(buffer, 0, buffer.Length);
            } while (bytesRead > 0);
        }

        public async Task SendAsync(IMessageReader reader)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[1024];
            do
            {
                bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length);
            } while (bytesRead > 0);
        }
    }
}
