using Cyrus.Channels;
using System;
using System.Threading.Tasks;

namespace Cyrus
{
    public class PassThroughProcessor : IProcessor
    {
        public IReceiveChannel RequestChannel { get; }
        public ISendChannel ReplyChannel { get; }

        public PassThroughProcessor(IReceiveChannel requestChannel, ISendChannel replyChannel)
        {
            RequestChannel = requestChannel;
            ReplyChannel = replyChannel;
        }

        public async Task ProcessAsync()
        {
            await ReplyChannel.SendAsync(await RequestChannel.ReceiveAsync());
        }        
    }
}
