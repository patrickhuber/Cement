using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Cyrus.AzureKeyVault.Tests.Integration
{
    [TestClass]
    public class AzureKeyVaultSecretStoreTests
    {
        IConfiguration Configuration { get; set; }

        [TestInitialize]
        public void InitializeAzureKeyVaultSecretStoreTests()
        {
            var builder = new ConfigurationBuilder();
            Configuration = builder.Build();
        }

        [TestMethod]
        public async Task AzureKeyVaultSecretStoreShouldAllowSecretLifeCycle()
        {
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(GetToken));
            var secretUri = Configuration["SecretUri"];
            var secretStore = new AzureKeyVaultSecretStore(keyVaultClient, new Uri(secretUri));
            var secret = await secretStore.ReadSecretAsync("somePath");
            Assert.IsNotNull(secret);
        }

        public async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authenticationContext = new AuthenticationContext(authority);
            var clientCredential = new ClientCredential(
                Configuration["ClientId"],
                Configuration["ClientSecret"]);
            var authenticationResult = await authenticationContext.AcquireTokenAsync(
                resource, 
                clientCredential);

            if (authenticationResult == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return authenticationResult.AccessToken;
        }
    }
}
