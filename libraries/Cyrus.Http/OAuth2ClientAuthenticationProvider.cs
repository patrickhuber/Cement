using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Cyrus.Http
{
    public class OAuth2ClientAuthenticationProvider : AuthenticationProvider<OAuth2ClientCredential>
    {
        public override AuthenticationHeaderValue Handle(OAuth2ClientCredential credential)
        {
            throw new NotImplementedException();
        }
    }
}
