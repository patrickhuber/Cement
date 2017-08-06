using System;
using System.Net.Http.Headers;
using System.Text;

namespace Cyrus.Http
{
    public class BasicAuthenticationProvider 
        : AuthenticationProvider<BasicAuthenticationCredential>
    {
        public override AuthenticationHeaderValue Handle(BasicAuthenticationCredential credential)
        {
            return new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes($"{credential.UserName}:{credential.Password}")));
        }
    }
}
