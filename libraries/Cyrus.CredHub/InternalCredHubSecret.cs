using Newtonsoft.Json;

namespace Cyrus.CredHub
{
    [JsonObject]
    internal class InternalCredHubSecret
    {
        [JsonProperty]
        public InternalCredHubSecretVersion[] data { get; set; }
    }
}
