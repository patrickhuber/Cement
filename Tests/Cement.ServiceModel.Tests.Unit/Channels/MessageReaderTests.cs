using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cement.ServiceModel.Channels;
using System.IO;
using System.ServiceModel.Channels;
using Moq;
using System.Text;

namespace Cement.ServiceModel.Tests.Unit.Channels
{
    [TestClass]
    public class MessageReaderTests
    {
        Stream stream;
        BufferManager bufferManager;
        IMessageEncoder messageEncoder;

        [TestInitialize]
        public void Initialize_MessageReader_Tests()
        {            
            var buffer = Encoding.ASCII.GetBytes("This is the text to encode.");
            stream = new MemoryStream(buffer);

            bufferManager = BufferManager.CreateBufferManager(524288, 65536);

            var mockMessageEncoder = new Mock<IMessageEncoder>();
            mockMessageEncoder.Setup(x=>x.ReadMessage(It.IsAny<Stream>(), It.IsAny<int>()))
                .Returns(()=>new MockMessage());
            messageEncoder = mockMessageEncoder.Object;
        }

        [TestMethod]
        public void Tests_MessageReader_Read_Streams_Data()
        {
            var messageReader = new MessageReader(
                stream,
                bufferManager,
                messageEncoder,
                2000000000); 
            var message = messageReader.ReadStreamed();            
            Assert.IsNotNull(message);
            Assert.AreEqual(0, stream.Position);
        }
    }
}
