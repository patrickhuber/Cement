using Cyrus.Adapters;
using System;
using Cyrus.Channels;
using System.Threading.Tasks;
using System.Net.Http;
using Cyrus.Messages;
using System.Collections.Generic;
using Cyrus.Http.Messages;

namespace Cyrus.Http.Adapters
{
    public class HttpRequstReplyAdapter : IRequestReplyAdapter
    {
        public IReceiveChannel RequestChannel { get; private set; }

        public ISendChannel ReplyChannel { get; private set; }

        public HttpClient _httpClient;

        public string Endpoint { get; private set; }

        public HttpMethod Method { get; private set; }

        public HttpRequstReplyAdapter(
            HttpClient client, 
            string endpoint,
            HttpMethod method,
            IReceiveChannel requestChannel, 
            ISendChannel replyChannel)
        {
            _httpClient = client;
            Endpoint = endpoint;
            Method = method;

            RequestChannel = requestChannel;
            ReplyChannel = replyChannel;
        }

        public async Task SendAsync()
        {
            // get the message from the request channel
            using (var message = RequestChannel.Receive())
            {
                var httpRequestMessage = new HttpRequestMessage(Method, Endpoint);

                // get the settings from the adapter to apply to the request message
                var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

                // create a new message with the response message 
                var responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var replyMessage = new Message(responseStream, GetMessageHeadersFromHttpResponseMessage(httpResponseMessage));

                // and send the message
                await ReplyChannel.SendAsync(replyMessage);
            }
        }

        private IDictionary<string, string> GetMessageHeadersFromHttpResponseMessage(HttpResponseMessage httpResponseMessage)
        {
            return new Dictionary<string, string>
            {
                { MessageProperties.Id, Guid.NewGuid().ToString() },
                { HttpResponseProperties.HttpHeaders, httpResponseMessage.Headers.ToString() },
                { HttpResponseProperties.StatusCode, httpResponseMessage.StatusCode.ToString() },
                { HttpResponseProperties.HttpMethod, httpResponseMessage.RequestMessage.Method.Method }
            };
        }
    }
}
