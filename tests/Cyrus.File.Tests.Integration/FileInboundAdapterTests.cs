using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.File.Tests.Integration
{
    [TestClass]
    public class FileInboundAdapterTests
    {
        [TestMethod]
        public async Task FileInboundAdapterShouldReceiveFiles()
        {
            var sendChannel = new InMemoryChannel();            

            var path = Path.Combine(Directory.GetCurrentDirectory(), "send");
                        
            var directoryName = Guid.NewGuid().ToString();
            var directoryPath = Path.Combine(path, directoryName);
            var directoryInfo = Directory.CreateDirectory(directoryPath);

            var fileName = Guid.NewGuid().ToString() + ".txt";
            var filePath = Path.Combine(directoryPath, fileName);

            using (var file = System.IO.File.Create(filePath))
            {
                var streamWriter = new StreamWriter(file);
                await streamWriter.WriteAsync("this is some data");
            }

            var inboundAdapter = new FileInboundAdapter(
                new FileSystem(),
                directoryPath, 
                "*.*", 
                sendChannel);

            await inboundAdapter.ReceiveAsync();

            Assert.AreEqual(1, sendChannel.Count);
        }
    }
}
