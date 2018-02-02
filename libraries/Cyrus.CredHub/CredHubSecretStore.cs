using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CredHub
{
    class CredHubSecretStore : ISecretStore
    {
        public Task<string> ReadSecretAsync(string path)
        {
            throw new NotImplementedException();
        }
    }
}
