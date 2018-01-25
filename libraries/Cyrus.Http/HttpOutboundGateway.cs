using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cyrus.Http
{
    public class HttpOutboundGateway : IOutboundGateway
    {
        public IReceiveChannel InboundChannel { get; private set; }

        public ISendChannel OutboundChannel { get; private set; }

        public HttpOutboundGatewaySettings Settings { get; private set; }

        private HttpClient _httpClient;

        public HttpOutboundGateway(
            HttpClient client,
            HttpOutboundGatewaySettings settings,
            IReceiveChannel inboundChannel,
            ISendChannel outboundChannel)
        {
            _httpClient = client;

            Settings = settings;

            InboundChannel = inboundChannel;
            OutboundChannel = outboundChannel;
        }

        public async Task SendAsync()
        {
            // get the message from the request channel
            using (var message = InboundChannel.Receive())
            {
                var httpRequestMessage = new HttpRequestMessage(
                    Settings.Method,
                    Settings.Endpoint);

                if (Settings.Authentication != null)
                    httpRequestMessage.Headers.Authorization = Settings.Authentication;

                // get the settings from the adapter to apply to the request message
                var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

                // create a new message with the response message 
                var responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var replyMessage = new Message(responseStream, GetMessageHeadersFromHttpResponseMessage(httpResponseMessage));

                // and send the message
                await OutboundChannel.SendAsync(replyMessage);
            }
        }

        private IDictionary<string, string> GetMessageHeadersFromHttpResponseMessage(HttpResponseMessage httpResponseMessage)
        {
            return new Dictionary<string, string>
            {
                { MessageProperties.Id, Guid.NewGuid().ToString() },
                { HttpResponseProperties.HttpHeaders, httpResponseMessage.Headers.ToString() },
                { HttpResponseProperties.StatusCode, ((int)httpResponseMessage.StatusCode).ToString() },
                { HttpResponseProperties.HttpMethod, httpResponseMessage.RequestMessage.Method.Method }
            };
        }

    }
}
