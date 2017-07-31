using System;
using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Pulls a message from a receive channel, disposing of the message and reading its body.
    /// </summary>
    public class NullSink : ISink
    {
        public IReceiveChannel ReceiveChannel { get; }

        public NullSink(IReceiveChannel receiveChannel)
        {
            ReceiveChannel = receiveChannel;
        }

        public async Task SendAsync()
        {
            using (var message = ReceiveChannel.Receive())
            {
                var buffer = new byte[1024];
                var bytesRead = 0;
                do
                {
                    bytesRead = await message.ReadAsync(buffer, 0, buffer.Length);
                } while (bytesRead > 0);
            }
        }

    }
}
