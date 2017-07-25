using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Cyrus.Channels;
using Cyrus.Messages;

namespace Cyrus.Tests.Unit.Channels
{
    [TestClass]
    public class InMemoryChannelTests
    {
        [TestMethod]
        public void TestInMemoryChannelSendShouldAddMessageToQueue()
        {
            var body = new MemoryStream(Encoding.ASCII.GetBytes("0123456789"));
            var message = new Message(body , new Dictionary<string, string> {
                { "prop1", "value1" }
            });
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
            var body = new MemoryStream(Encoding.ASCII.GetBytes("0123456789"));
            var message = new Message(body, new Dictionary<string, string> { { "prop1", "value1" } });
            var channel = new InMemoryChannel();
            channel.Send(message);

            using (var receivedMessage = channel.Receive())
            {
            }

            Assert.AreEqual(0, channel.Count);
        }
    }
}
