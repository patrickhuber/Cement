using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Cyrus
{
    public class ConfigurationSecretStore : ISecretStore
    {
        public IConfiguration Configuration { get; private set; }

        public ConfigurationSecretStore(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Task<string> ReadSecretAsync(string path)
        {
            return Task.FromResult(Configuration[path]);
        }
    }
}
