namespace Cyrus.Http
{
    public class OAuth2PasswordCredential
    {
        public string UserName { get; }
        public string Password { get; }
        public string ClientId { get; }

        public OAuth2PasswordCredential(
            string userName,
            string password,
            string clientId)
        {
            ClientId = clientId;
            Password = password;
            UserName = userName;
        }
    }
}
