using Microsoft.Net.Http.Server;
using System.Threading.Tasks;

namespace Cyrus.Http
{
    public class HttpInboundGateway : IInboundGateway
    {
        public IReceiveChannel InboundChannel { get; }

        public ISendChannel OutboundChannel { get; }

        private WebListener _webListener;

        public HttpInboundGateway(
            WebListener server,
            IReceiveChannel inboundChannel,
            ISendChannel outboundChannel)
        {
            InboundChannel = inboundChannel;
            OutboundChannel = outboundChannel;
            _webListener = server;            
        }

        public async Task ReceiveAsync()
        {
            var requestContext = await _webListener.AcceptAsync();            
        }
    }
}
