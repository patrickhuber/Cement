using System.Threading.Tasks;

namespace Cyrus
{
    public interface ISecretStore
    {
        Task<string> ReadSecretAsync(string path);
    }
}
