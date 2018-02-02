using Newtonsoft.Json;
using System;

namespace Cyrus.CredHub
{
    [JsonObject]
    internal class InternalCredHubSecretVersion
    {
        [JsonProperty]
        public string id { get; set; }
        [JsonProperty]
        public string name { get; set; }
        [JsonProperty]
        public string type { get; set; }
        [JsonProperty]
        public string value { get; set; }
        [JsonProperty]
        public DateTime version_created_at { get; set; }
    }
}
