using Cyrus.Channels;
using System;
using System.Threading.Tasks;

namespace Cyrus.Adapters
{
    /// <summary>
    /// Pulls a message from a receive channel, disposing of the message and reading its body.
    /// </summary>
    public class NullOutboundAdapter : IOutboundAdapter
    {
        public IReceiveChannel InboundChannel { get; }

        public NullOutboundAdapter(IReceiveChannel receiveChannel)
        {
            InboundChannel = receiveChannel;
        }

        public async Task SendAsync()
        {
            using (var message = await InboundChannel.ReceiveAsync())
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
