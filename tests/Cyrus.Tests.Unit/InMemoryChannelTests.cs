using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Cyrus.Tests.Unit
{
    [TestClass]
    public class InMemoryChannelTests
    {
        [TestMethod]
        public void TestInMemoryChannelSendShouldAddMessageToQueue()
        {
            var message = MessageHelper.Create();
            var channel = new InMemoryChannel();
            channel.Send(message);

            Assert.AreEqual(1, channel.Count);
            using (var receivedMessage = channel.Receive())
            {
                Assert.AreEqual(receivedMessage.MessageHeader.Count, 1);
                Assert.AreEqual(receivedMessage.MessageHeader["prop1"], "value1");
            }
        }

        [TestMethod]
        public void TestInMemoryChannelReceiveShouldRemoveMessageFromQueue()
        {
            var message = MessageHelper.Create();
            var channel = new InMemoryChannel();
            channel.Send(message);

            using (var receivedMessage = channel.Receive())
            {
            }

            Assert.AreEqual(0, channel.Count);
        }
    }
}
