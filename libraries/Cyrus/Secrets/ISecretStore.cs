using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Secrets
{
    public interface ISecretStore
    {
        Task<string> ReadSecretAsync(string path);
    }
}
