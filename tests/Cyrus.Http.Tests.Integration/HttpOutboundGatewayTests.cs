using Cyrus.Channels;
using Cyrus.Http.Gateways;
using Cyrus.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cyrus.Http.Tests.Integration.Adapters
{
    [TestClass]
    public class HttpOutboundGatewayTests
    {
        IConfiguration Configuration { get; set; }
        ConfigurationSecretStore SecretStore { get; set; }

        public HttpOutboundGatewayTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<HttpOutboundGatewayTests>();
            Configuration = builder.Build();
            SecretStore = new ConfigurationSecretStore(Configuration);
        }

        [TestMethod]
        public async Task HttpOutboundGatewaySendShouldCreateNewMessageWithHttpResponse()
        {
            var settings = new HttpOutboundGatewaySettings(
                "https://www.google.com",
                HttpMethod.Get);         

            await TestAsync(settings, m =>
            {
                Assert.AreEqual("200", m.MessageHeader[HttpResponseProperties.StatusCode]);
            });
        }

        [TestMethod]
        public async Task HttpOutboundGatewaySendShouldWorkWithTokenAuthorization()
        {
            var tokenPath = "/HttpRequestReplyAdapterTests/PivotalNetworkToken";
            var tokenCredentialStore = new TokenCredentialStore(SecretStore);
            var tokenAuthenticationProvider = new TokenAuthenticationProvider();
            var settings = new HttpOutboundGatewaySettings(
                    "https://network.pivotal.io/api/v2/authentication",
                    HttpMethod.Get,
                    "application/json",
                    tokenAuthenticationProvider.Handle(
                        await tokenCredentialStore.GetAsync(tokenPath)));

            await TestAsync(settings, m=> 
            {
                Assert.AreEqual("200", m.MessageHeader[HttpResponseProperties.StatusCode]);
            });
        }

        [TestMethod]
        public async Task HttpOutboundGatewaySendShouldWorkWithBasicAuthentication()
        {
            var username = "username";
            var password = "password";

            var authenticationProvider = new BasicAuthenticationProvider();

            var settings = new HttpOutboundGatewaySettings(
                $"http://httpbin.org/basic-auth/{username}/{password}",
                HttpMethod.Get,
                "text/xml",               
                    authenticationProvider.Handle(
                        new BasicAuthenticationCredential(username, password))
                );
            await TestAsync(settings, m =>
            {
                Assert.AreEqual("200", m.MessageHeader[HttpResponseProperties.StatusCode]);
            });
        }

        private async Task TestAsync(HttpOutboundGatewaySettings settings, Action<IMessageReader> assert)
        {
            var requestChannel = new InMemoryChannel();
            var replyChannel = new InMemoryChannel();

            var httpOutboundGateway = new HttpOutboundGateway(
                new HttpClient(),
                settings,
                requestChannel,
                replyChannel);

            // create the empty message and add it to the request channel
            var emptyMessage = new Message(new MemoryStream(), new Dictionary<string, string> { });
            await requestChannel.SendAsync(emptyMessage);

            // send the message
            await httpOutboundGateway.SendAsync();

            // pull the message off of the reply channel
            using (var message = await replyChannel.ReceiveAsync())
            {
                assert(message);
            }
        }
    }

}
