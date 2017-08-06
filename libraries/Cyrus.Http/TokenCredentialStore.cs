using System;
using System.Threading.Tasks;

namespace Cyrus.Http
{
    public class TokenCredentialStore : CredentialStoreBase<TokenCredential>
    {
        public TokenCredentialStore(ISecretStore secretStore)
            :base(secretStore)
        {     
        }

        public override async Task<TokenCredential> GetAsync(string path)
        {
            var token = await SecretStore.ReadSecretAsync(path);
            return new TokenCredential(token);
        }
    }
}
