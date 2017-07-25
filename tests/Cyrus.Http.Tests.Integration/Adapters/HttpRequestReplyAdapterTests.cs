using Cyrus.Channels;
using Cyrus.Http.Adapters;
using Cyrus.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Http.Tests.Integration.Adapters
{
    [TestClass]
    public class HttpRequestReplyAdapterTests
    {
        [TestMethod]
        public async Task HttpRequestReplySendShouldCreateNewMessageWithHttpResponse()
        {
            var requestChannel = new InMemoryChannel();
            var replyChannel = new InMemoryChannel();
            var httpRequestReplyAdapter = new HttpRequstReplyAdapter(
                new HttpClient(),
                "https://www.google.com",
                HttpMethod.Get,
                requestChannel,
                replyChannel);

            // create the empty message and add it to the request channel
            var emptyMessage = new Message(new MemoryStream(), new Dictionary<string, string> { });
            await requestChannel.SendAsync(emptyMessage);

            // send the message
            await httpRequestReplyAdapter.SendAsync();

            using (var message = replyChannel.Receive())
            {
                var memoryStream = new MemoryStream();
                await Message.CopyToAsync(message, memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var array = memoryStream.ToArray();
                var content = Encoding.UTF8.GetString(array);
                Assert.IsTrue(content.Contains("<html"));
            }
            
        }
    }

}
