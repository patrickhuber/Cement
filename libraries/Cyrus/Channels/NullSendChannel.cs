using Cyrus.Channels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cyrus.Channels
{
    public class NullSendChannel : ISendChannel
    {
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
