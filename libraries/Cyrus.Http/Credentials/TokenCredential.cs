using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrus.Http.Credentials
{
    public class TokenCredential
    {
        public string Token { get; private set; }

        public TokenCredential(string token)
        {
            Token = token;
        }
    }
}
