using Cyrus.Adapters;
using Cyrus.Channels;
using Cyrus.Http.Credentials;
using Cyrus.Http.Messages;
using Cyrus.Messages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Http.Adapters
{
    public class HttpRequestReplyAdapter : IRequestReplyAdapter
    {
        public IReceiveChannel RequestChannel { get; private set; }

        public ISendChannel ReplyChannel { get; private set; }
        
        public HttpRequestReplyAdapterSettings Settings { get; private set; }
        
        private HttpClient _httpClient;

        public HttpRequestReplyAdapter(
            HttpClient client, 
            HttpRequestReplyAdapterSettings settings,
            IReceiveChannel requestChannel, 
            ISendChannel replyChannel)
        {
            _httpClient = client;

            Settings = settings;

            RequestChannel = requestChannel;
            ReplyChannel = replyChannel;
        }

        public async Task SendAsync()
        {            
            // get the message from the request channel
            using (var message = RequestChannel.Receive())
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
                await ReplyChannel.SendAsync(replyMessage);
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

        public AuthenticationHeaderValue GetBasicAuthenticationHeader(
            BasicAuthenticationCredential credential)
        {
            var credentialString = $"{credential.UserName}:{credential.Password}";
            var credentialBytes = Encoding.ASCII.GetBytes(credentialString);
            var credentialBase64 = Convert.ToBase64String(credentialBytes);
            var authenticationHeaderValue = new AuthenticationHeaderValue("Basic", credentialBase64);
            return authenticationHeaderValue;
        }


    }
}
