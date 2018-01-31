using System.Net.Http;
using System.Net.Http.Headers;

namespace Cyrus.Http.Gateways
{
    public class HttpOutboundGatewaySettings
    {
        public string Endpoint { get; private set; }

        public HttpMethod Method { get; private set; }

        public string ContentType { get; private set; }

        public AuthenticationHeaderValue Authentication { get; private set; }

        public HttpRequestHeaders Headers { get; private set; }

        public HttpOutboundGatewaySettings(
            string endpoint,
            HttpMethod httpMethod,
            string contentType = "text/xml",
            AuthenticationHeaderValue authenticationHeaderValue = null,
            HttpRequestHeaders headers = null)
        {
            Endpoint = endpoint;
            Method = httpMethod;
            ContentType = contentType;
            Authentication = authenticationHeaderValue;
            Headers = headers;
        }
    }
}
