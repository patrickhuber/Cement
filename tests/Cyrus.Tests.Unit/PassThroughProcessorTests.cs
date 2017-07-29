using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Tests.Unit
{
    [TestClass]
    public class PassThroughProcessorTests
    {
        [TestMethod]
        public async Task PassThroughProcessorSendShouldReceiveAndSendMessage()
        {
            var actualContent = "test";
            var actualPropertyValue = "something";
            var actualPropertyKey = "key";
            var receiveChannel = new InMemoryChannel();
            var sendChannel = new InMemoryChannel();

            var passThroughProcessor = new PassThroughProcessor(
                receiveChannel,
                sendChannel);

            // create the message and pass it to the receive channel             
            var message = MessageHelper.Create(
                actualContent, 
                new Dictionary<string, string> { { actualPropertyKey, actualPropertyValue } });
            await receiveChannel.SendAsync(message);

            // run the processor
            await passThroughProcessor.SendAsync();

            // get the message from the send channel
            var passedMessage = sendChannel.Receive();

            // verify the headers
            var header = passedMessage.MessageHeader;
            Assert.AreEqual(1, header.Count);
            Assert.AreEqual(actualPropertyValue, header[actualPropertyKey]);

            // copy message to stream for string translation
            var memoryStream = new MemoryStream();
            await Message.CopyToAsync(passedMessage, memoryStream);

            // body should be the same
            Assert.AreEqual(Encoding.ASCII.GetString(memoryStream.ToArray()), actualContent);
        }
    }
}
