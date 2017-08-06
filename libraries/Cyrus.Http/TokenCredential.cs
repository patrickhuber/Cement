namespace Cyrus.Http
{
    public class TokenCredential : CredentialBase
    {
        public string Token { get; private set; }

        public TokenCredential(string token)
        {
            Token = token;
        }
    }
}
