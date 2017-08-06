using System;
using System.Net.Http.Headers;

namespace Cyrus.Http
{
    public abstract class AuthenticationProvider<T> 
        : IAuthenticationProvider<T>
        where T : CredentialBase
    {
        public Type CredentialType => typeof(T).GetType();

        public abstract AuthenticationHeaderValue Handle(T credential);
    }
}
