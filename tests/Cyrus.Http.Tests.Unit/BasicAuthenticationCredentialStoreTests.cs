using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Http.Tests.Unit
{
    [TestClass]
    public class BasicAuthenticationCredentialStoreTests
    {
        [TestMethod]
        public async Task BasicAuthenticationCredentialStoreGetAsyncShouldReturnBasicAuthenticationCredential()
        {
            var path = "/some/path/to/a/credential";
            var expectedPassword = "password";
            var expectedUsernName = "username";
            await RunTest(path, expectedPassword, expectedUsernName);
        }

        [TestMethod]
        public async Task BasicAuthenticationCredentialStoreGetAsyncShouldReturnBasicAuthenticationCredentialWhenPathEndsInSlash()
        {
            var path = "/some/path/to/a/credential/";
            var expectedPassword = "password";
            var expectedUsernName = "username";
            await RunTest(path, expectedPassword, expectedUsernName);
        }

        private static async Task RunTest(string path, string expectedPassword, string expectedUsernName)
        {
            var pathEnd = path.EndsWith("/", StringComparison.CurrentCulture)
                ? ""
                : "/";
            var basicAuthenticationCredentialStore = new BasicAuthenticationCredentialStore(
                            new InMemorySecretStore(
                                new Dictionary<string, string>
                                {
                        { $"{path}{pathEnd}{BasicAuthenticationCredentialStore.UserNameKey}", expectedUsernName },
                        { $"{path}{pathEnd}{BasicAuthenticationCredentialStore.PasswordKey}", expectedPassword }
                                }));
            var credential = await basicAuthenticationCredentialStore.GetAsync(path);
            Assert.AreEqual(expectedUsernName, credential.UserName);
            Assert.AreEqual(expectedPassword, credential.Password);
        }
    }
}
