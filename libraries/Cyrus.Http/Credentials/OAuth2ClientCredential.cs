using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrus.Http.Credentials
{
    class OAuth2ClientCredential
    {
        public string ClientSecret { get; }
        public string ClientId { get; }

        public OAuth2ClientCredential(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}
