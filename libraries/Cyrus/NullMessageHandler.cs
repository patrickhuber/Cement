using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    public class NullMessageHandler : IMessageHandler
    {
        public void Handle(IMessageReader message)
        {
            byte[] buffer = new byte[1024];
            var bytesRead = 0;
            using (message)
            {
                do
                {
                    bytesRead = message.Read(buffer, bytesRead, buffer.Length);
                } while (bytesRead > 0);
            }
        }

        public async Task HandleAsync(IMessageReader message)
        {
            byte[] buffer = new byte[1024];
            var bytesRead = 0;
            using (message)
            {
                do
                {
                    bytesRead = await message.ReadAsync(buffer, bytesRead, buffer.Length);
                } while (bytesRead > 0);
            }
        }
    }
}
