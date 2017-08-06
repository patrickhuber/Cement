using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Cyrus.Http
{
    public class TokenAuthenticationProvider : AuthenticationProvider<TokenCredential>
    {
        public override AuthenticationHeaderValue Handle(TokenCredential credential)
        {
            return new AuthenticationHeaderValue("Token", credential.Token);
        }
    }
}
