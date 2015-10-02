using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Cyrus.Tests.Unit
{
    /// <summary>
    /// Summary description for InMemoryChannelTests
    /// </summary>
    [TestClass]
    public class InMemoryChannelTests
    {
        public InMemoryChannelTests()
        {
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        

        [TestMethod]
        public void Test_InMemoryChannel_That_Send_Reads_The_Message_Body()
        {
            var message = new Message(new MemoryStream(), new Dictionary<string, string> { });
            var channel = new InMemoryChannel();
            channel.Send(message);

            // the body length should be equal to its position
            Assert.AreEqual(message.Body.Length, message.Body.Position);
        }

        [TestMethod]
        public void Test_InMemoryChannel_That_Receive_Empty_Channel_Returns_Null()
        {
            var channel = new InMemoryChannel();
            Assert.IsNull(channel.Receive());
        }


        [TestMethod]
        public void Test_InMemoryChannel_That_Receive_Returns_Clean_Body_Stream()
        {
            var sentMessage = new Message(new MemoryStream(), new Dictionary<string, string> { });
            var channel = new InMemoryChannel();
            channel.Send(sentMessage);

            var receivedMessage = channel.Receive();
            Assert.AreEqual(0, receivedMessage.Body.Length);
        }
    }
}
