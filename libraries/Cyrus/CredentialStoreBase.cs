
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    public abstract class CredentialStoreBase<T> : ICredentialStore<T>
        where T : CredentialBase
    {
        public ISecretStore SecretStore { get; }

        protected CredentialStoreBase(ISecretStore secretStore)
        {
            SecretStore = secretStore;
        }

        public abstract Task<T> GetAsync(string path);
    }
}
