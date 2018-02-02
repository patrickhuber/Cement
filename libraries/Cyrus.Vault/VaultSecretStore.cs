using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp;

namespace Cyrus.Vault
{
    public class VaultSecretStore : ISecretStore
    {
        public IVaultClient Client { get; }

        public VaultSecretStore(IVaultClient client)
        {
            Client = client;
        }

        public async Task<string> ReadSecretAsync(string path)
        {
            var secret = await Client.ReadSecretAsync(path);
            var data = secret.Data;
            foreach (var key in data.Keys)
                return data[key] as string;
            throw new InvalidOperationException($"No Secret Data present for path {path}");
        }

        public async Task WriteSecretAsync(string path, string secret)
        {
            await Client.WriteSecretAsync(
                path, 
                new Dictionary<string, object>
                {
                    { "", secret }
                });
        }
    }
}
