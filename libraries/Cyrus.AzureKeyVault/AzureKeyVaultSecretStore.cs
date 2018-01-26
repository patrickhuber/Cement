using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cyrus.AzureKeyVault
{
    public class AzureKeyVaultSecretStore : ISecretStore
    {
        public IKeyVaultClient KeyVaultClient { get; private set; }
        public Uri Uri { get; private set; }

        private static readonly Regex PathValidator = new Regex(@"[-a-zA-Z0-9]");
        
        public AzureKeyVaultSecretStore(IKeyVaultClient keyVaultClient, Uri uri)
        {
            Uri = uri;
            KeyVaultClient = keyVaultClient;
        }

        public async Task<string> ReadSecretAsync(string path)
        {
            if (!PathValidator.IsMatch(path))
                throw new ArgumentException($"Argument {nameof(path)} is invalid.", nameof(path));

            var key = await KeyVaultClient.GetSecretAsync(Uri.ToString(), path);
            return key.Value;
        }

        private async Task<string> GetTokenAsync(
            string authority, 
            string resource, 
            string scope, 
            string clientId, 
            string clientSecret)
        {
            var authenticationContext = new AuthenticationContext(authority);
            var clientCredential = new ClientCredential(clientId, clientSecret);
            var authenticationResult = await authenticationContext.AcquireTokenAsync(
                resource,
                clientCredential);
            if (authenticationResult == null)
                throw new InvalidOperationException("Failed to obtain JWT token");
            return authenticationResult.AccessToken;
        }
    }
}
