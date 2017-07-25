using Cyrus.Channels;
using Cyrus.IO;
using Cyrus.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Tests.Integration.IO
{
    [TestClass]
    public class FileSendAdapterTests
    {
        
        [TestMethod]
        public async Task FileSendAdapterSendShouldWriteFile()
        {
            var mockReceiveChannel = new Mock<IReceiveChannel>();
            mockReceiveChannel
                .Setup(x => x.Receive()).
                Returns(
                    new Message(
                        new MemoryStream(Encoding.ASCII.GetBytes("0123456789")),
                        new Dictionary<string, string> { }));
            var fileSendAdapter = new FileSendAdapter(
                new FileSystem(), 
                Directory.GetCurrentDirectory(),
                mockReceiveChannel.Object);

            await fileSendAdapter.SendAsync();

            var file = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.txt").First();
            File.Delete(file);
        }
    }
}
