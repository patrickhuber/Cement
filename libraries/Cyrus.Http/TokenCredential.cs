namespace Cyrus.Http
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
