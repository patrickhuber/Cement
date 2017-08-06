using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    public class InMemorySecretStore : ISecretStore
    {
        private IDictionary<string, string> _secrets;
        
        public InMemorySecretStore(IDictionary<string, string> secrets)
        {
            _secrets = new Dictionary<string, string>(secrets);
        }

        public Task<string> ReadSecretAsync(string path)
        {
            return Task.FromResult(_secrets[path]);
        }
    }
}
