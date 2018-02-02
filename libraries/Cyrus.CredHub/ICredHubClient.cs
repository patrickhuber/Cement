using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CredHub
{
    public interface ICredHubClient
    {
        Task<CredHubSecret> GetDataByNameAsync(string name, bool current = false, int versions = 1);
        Task<CredHubSecretVersion> GetDataByIdAsync(string name);
    }
}
