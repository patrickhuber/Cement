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
        Stream memoryStream;
        IBufferManager bufferManager;
        IMessageEncoder messageEncoder;

        [TestInitialize]
        public void Initialize_MessageReader_Tests()
        {
            var buffer = Encoding.ASCII.GetBytes("This is the text to encode.");
            memoryStream = new MemoryStream(buffer);

            bufferManager = new BufferManagerAdapter(
                BufferManager.CreateBufferManager(524288, 65536));

            var mockMessageEncoder = new Mock<IMessageEncoder>();
            mockMessageEncoder
                .Setup(x => x.ReadMessage(It.IsAny<ArraySegment<byte>>(), It.IsAny<IBufferManager>()))
                .Returns<ArraySegment<byte>, IBufferManager>((a, bm) =>
                {
                    return Message.CreateMessage(MessageVersion.Soap11WSAddressing10, "Action", Encoding.ASCII.GetString(a.Array));
                });
            messageEncoder = mockMessageEncoder.Object;
        }

        [TestMethod]
        public void Tests_MessageReader_Read_Streams_Data()
        {
            var messageReader = new MessageReader(
                memoryStream,
                bufferManager,
                messageEncoder,
                2000000000);
            var message = messageReader.Read();
            Assert.IsNotNull(message);
        }
    }
}
