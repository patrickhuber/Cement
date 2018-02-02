using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrus.CredHub
{
    public class CredHubSecret
    {
        public IReadOnlyList<CredHubSecretVersion> Versions { get; }

        public CredHubSecret(params CredHubSecretVersion[] versions)
        {
            Versions = new List<CredHubSecretVersion>(versions);
        }

        public CredHubSecret(IEnumerable<CredHubSecretVersion> versions)
        {
            Versions = new List<CredHubSecretVersion>(versions);
        }
    }
}
