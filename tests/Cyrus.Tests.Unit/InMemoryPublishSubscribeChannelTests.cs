using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Linq.Expressions;

namespace Cyrus.Tests.Unit
{
    [TestClass]
    public class InMemoryPublishSubscribeChannelTests
    {
        [TestMethod]
        public async Task InMemoryPublishSubscribeChannelShouldBroadcastMessage()
        {
            var subscriberCount = 2;

            var originalMessageId = Guid.NewGuid().ToString();

            var message = new Message(
                new MemoryStream(
                    Encoding.ASCII.GetBytes("")),
                new Dictionary<string, string>()
                {
                    { "header1","value1" },
                    { MessageProperties.Id, originalMessageId }
                });
                        
            await Publish(subscriberCount, message);
        }

        [TestMethod]
        public async Task InMemoryPublishSubscribeChannelEachMessageShouldHaveUniqueMessageIdentifier()
        {
            var subscriberCount = 2;

            var originalMessageId = Guid.NewGuid().ToString();

            var message = new Message(
                new MemoryStream(
                    Encoding.ASCII.GetBytes("")),
                new Dictionary<string, string>()
                {
                    { "header1","value1" },
                    { MessageProperties.Id, originalMessageId }
                });

            Func<IMessageReader, Task> onMessageHandled = reader =>
            {
                Assert.IsTrue(reader.MessageHeader.ContainsKey("header1"));
                Assert.AreNotEqual(originalMessageId, reader.MessageHeader[MessageProperties.Id]);
                return Task.FromResult(1);
            };
            await Publish(subscriberCount, message, onMessageHandled);
        }

        private static async Task Publish(
            int subscriberCount, 
            Message message, 
            Func<IMessageReader, Task> onMessageHandled=null)
        {
            if (onMessageHandled == null)
                onMessageHandled = reader => Task.FromResult(1);

            var pubSubChannel = new InMemoryPublishSubscribeChannel();

            Expression<Func<IMessageHandler, Task>> expression =
                x => x.HandleAsync(It.IsAny<IMessageReader>());

            var mockMessageHandlers = new List<Mock<IMessageHandler>>();            
            for (var i = 0; i < subscriberCount; i++)
            {
                var mockMessageHandler = new Mock<IMessageHandler>();
                mockMessageHandler
                    .Setup(expression)
                    .Returns(onMessageHandled);
                pubSubChannel.Subscribe(mockMessageHandler.Object);
            }

            await pubSubChannel.SendAsync(message);

            foreach (var mockMessageHandler in mockMessageHandlers)
            {
                mockMessageHandler.Verify(expression, Times.Once);
            }
        }
    }
}
