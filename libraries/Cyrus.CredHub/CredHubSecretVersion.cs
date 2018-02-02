using System;
using System.Collections.Generic;

namespace Cyrus.CredHub
{
    public class CredHubSecretVersion
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Value { get; set; }
        public DateTimeOffset VersionCreatedAt { get; set; }
    }
}