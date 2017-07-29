using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Cyrus.File.Tests.Integration
{
    [TestClass]
    public class FileSendAdapterTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            DeploymentHelper.Deploy("1.txt", "send");
        }        

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

            var path = Path.Combine(Directory.GetCurrentDirectory(), "send");
            var fileSendAdapter = new FileSink(
                new FileSystem(),
                path,
                mockReceiveChannel.Object);

            await fileSendAdapter.SendAsync();

            var files = Directory.EnumerateFiles(
                path, "*.txt");
            foreach(var file in files)
                System.IO.File.Delete(file);
        }
    }
}
