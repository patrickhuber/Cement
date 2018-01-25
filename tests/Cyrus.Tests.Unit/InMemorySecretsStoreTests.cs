using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyrus.Tests.Unit
{
    [TestClass]
    public class InMemorySecretsStoreTests
    {
        [TestMethod]
        public void InMemorySecretsStoreShouldManageSecretLifeCycle()
        {
            var secretsStore = new InMemorySecretStore();
        }
    }
}
