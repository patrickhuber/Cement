using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CredHub
{
    public class CredHubClient : ICredHubClient
    {
        private HttpClient _httpClient;
        private string _baseUri;

        public CredHubClient(HttpClient httpClient, string baseUri)
        {
            _httpClient = httpClient;
            _baseUri = baseUri;
        }

        public async Task<CredHubSecretVersion> GetDataByIdAsync(string id)
        {
            var route = $"/api/v1/data/{id}";
            var requestUri = Join(_baseUri, route);

            var json = await _httpClient.GetStringAsync(requestUri);
            var internalCredHubSecretVersion = JsonConvert.DeserializeObject<InternalCredHubSecretVersion>(json);

            return MapInternalToPublic(internalCredHubSecretVersion);
        }

        private static CredHubSecret MapInternalToPublic(InternalCredHubSecret internalSecret)
        {
            var credHubSecretVersions = new List<CredHubSecretVersion>();
            foreach (var internalSecretVersion in internalSecret.data)
            {
                credHubSecretVersions.Add(
                    MapInternalToPublic(internalSecretVersion));
            }
            return new CredHubSecret(credHubSecretVersions);
        }


        private static CredHubSecretVersion MapInternalToPublic(InternalCredHubSecretVersion internalSecretVersion)
        {
            switch(internalSecretVersion.type)
            {
                case "password":
                    break;
                case "certificate":
                    break;
                case "value":
                    break;
                case "rsa":
                    break;
                case "json":
                    break;
                case "ssh":
                    break;
                case "user":
                    break;
            }
            throw new NotImplementedException();
        }

        public Task<CredHubSecret> GetDataByNameAsync(string name, bool current = false, int versions = 1)
        {
            throw new NotImplementedException();
        }

        private static string Join(string baseUri, string path)
        {
            return $"{baseUri.TrimEnd('/')}/{path.TrimStart('/')}";
        }
    }
}
