using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cyrus.Channels;

namespace Cyrus.Tests.Unit
{
    [TestClass]
    public class InMemoryChannelTests
    {
        [TestMethod]
        public async Task TestInMemoryChannelSendShouldAddMessageToQueue()
        {
            var message = MessageHelper.Create();
            var channel = new InMemoryChannel();
            channel.Send(message);

            Assert.AreEqual(1, channel.Count);
            using (var receivedMessage = await channel.ReceiveAsync())
            {
                Assert.AreEqual(receivedMessage.MessageHeader.Count, 1);
                Assert.AreEqual(receivedMessage.MessageHeader["prop1"], "value1");
            }
        }

        [TestMethod]
        public async Task TestInMemoryChannelReceiveShouldRemoveMessageFromQueue()
        {
            var message = MessageHelper.Create();
            var channel = new InMemoryChannel();
            channel.Send(message);

            using (var receivedMessage = await channel.ReceiveAsync())
            {
            }

            Assert.AreEqual(0, channel.Count);
        }
    }
}
