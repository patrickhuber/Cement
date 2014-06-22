using Cement.Adapters;
using Cement.Channels;
using Cement.IO;
using Cement.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Tests.Unit.Channels
{
    [TestClass]
    public class PassThroughChannelTests
    {
        public void Test_PassThroughChannel_Streams_Data()
        {
            var mockReceiveAdapter = new Mock<IReceiveAdapter>();
            var mockSendAdapter = new Mock<ISendAdapter>();
            
            var receiveAdapter = mockReceiveAdapter.Object;
            var sendAdapter = mockSendAdapter.Object;

            // the sender creates a message and writes that message to the channel
            // the channel has a list of receivers that it forwards the message to
            Assert.Fail();
        }

        [TestMethod]
        public void Test_PassThroughChannel_File_Move()
        {
            string expected = "this is the expected text.";
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(fs => fs.OpenRead(It.IsAny<string>()))
                .Returns(() => new MemoryStream(Encoding.ASCII.GetBytes(expected)));
            mockFileSystem
                .Setup(fs => fs.OpenWrite(It.IsAny<string>()))
                .Returns(() => new MemoryStream());

            var fileSystem = mockFileSystem.Object;
            using (var sourceStream = fileSystem.OpenRead(""))
            {
                using (var targetStream = fileSystem.OpenWrite(""))
                {
                    sourceStream.CopyTo(targetStream);
                    sourceStream.Seek(0, SeekOrigin.Begin);
                    var memorySteam = sourceStream as MemoryStream;
                    var bytes = memorySteam.ToArray();
                    Assert.AreEqual(expected, Encoding.ASCII.GetString(bytes));
                }
            }
        }

        [TestMethod]
        public void Test_PassThroughChannel_Channel_Receives_Push_Notifications()
        {
            
        }

    }
}
