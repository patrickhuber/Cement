using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus
{
    public interface ICredentialStore<T>
        where T : CredentialBase
    {
        Task<T> GetAsync(string path);
    }
}
