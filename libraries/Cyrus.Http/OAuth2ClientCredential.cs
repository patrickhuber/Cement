namespace Cyrus.Http
{
    public class OAuth2ClientCredential : CredentialBase
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
