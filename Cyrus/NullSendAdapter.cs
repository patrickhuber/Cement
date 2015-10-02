using System;
using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Null Send Adapter consumes a message and does nothing with the resultant message.
    /// </summary>
    public class NullSendAdapter : ISendAdapter
    {
        public NullSendAdapter(IChannel inputChannel)
        {
            InputChannel = inputChannel;
        }

        public IChannel InputChannel { get; private set; }

        /// <summary>
        /// Pulls a message from the channel and swallows the message.
        /// </summary>
        public void Send()
        {
            InputChannel.Receive();
        }

        /// <summary>
        /// Pulls a message from the channel and swallows the message.
        /// </summary>
        /// <returns>A task representing the work done by this method.</returns>
        public async Task SendAsync()
        {
            await InputChannel.ReceiveAsync();
        }
    }
}
