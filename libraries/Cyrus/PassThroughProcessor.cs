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

        public Task SendAsync()
        {
            return ReplyChannel.SendAsync(RequestChannel.Receive());
        }        
    }
}
