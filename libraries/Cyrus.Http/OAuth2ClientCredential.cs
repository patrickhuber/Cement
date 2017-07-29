namespace Cyrus.Http
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
