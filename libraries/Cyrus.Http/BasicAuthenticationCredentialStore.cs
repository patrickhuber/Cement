using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Http
{
    public class BasicAuthenticationCredentialStore : CredentialStoreBase<BasicAuthenticationCredential>
    {
        public static readonly string UserNameKey = "username";
        public static readonly string PasswordKey = "password";

        public BasicAuthenticationCredentialStore(ISecretStore secretStore)
            : base(secretStore)
        {
        }

        public override async Task<BasicAuthenticationCredential> GetAsync(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            
            var userNamePath = Append(path, UserNameKey);
            var passwordPath = Append(path, PasswordKey);

            var userName = await SecretStore.ReadSecretAsync(userNamePath);
            var password = await SecretStore.ReadSecretAsync(passwordPath);

            return new BasicAuthenticationCredential(userName, password);
        }

        private string Append(string path, string end)
        {
            var pathEndsWithSlash = path.EndsWith("/", StringComparison.CurrentCulture);
            if (pathEndsWithSlash)
                return$"{path}{end}";
            return $"{path}/{end}";
        }
    }
}
