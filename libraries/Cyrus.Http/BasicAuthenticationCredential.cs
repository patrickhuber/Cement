namespace Cyrus.Http
{
    public class BasicAuthenticationCredential
    {
        public string UserName { get; }
        public string Password { get; }

        public BasicAuthenticationCredential(string userName, string password)
        {
            Password = password;
            UserName = userName;
        }
    }
}
