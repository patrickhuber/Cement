using System;
using System.Net.Http.Headers;

namespace Cyrus.Http
{
    public interface IAuthenticationProvider<in T> : IAuthenticationProvider
        where T : CredentialBase
    {
        AuthenticationHeaderValue Handle(T credential);
    }

    public interface IAuthenticationProvider
    {
        Type CredentialType { get; }
    }
}