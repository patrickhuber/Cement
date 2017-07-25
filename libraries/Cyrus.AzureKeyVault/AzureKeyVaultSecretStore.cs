using Cyrus.Secrets;
using Microsoft.Azure.KeyVault;
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
    }
}
