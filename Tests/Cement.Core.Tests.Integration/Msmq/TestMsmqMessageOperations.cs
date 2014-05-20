using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Core.Tests.Integration.Msmq
{
    [TestClass]
    public class TestMsmqMessageOperations
    {
        const int MaxMessageSize = 4193849;
        const string PrivateQueueName = @".\private$\TestMessageStream";
        private static MessageQueue messageQueue;

        [ClassInitialize]
        public static void Initialize_MsmqMessageOperations(TestContext testContext)
        {
            if (MessageQueue.Exists(PrivateQueueName))
                messageQueue = new MessageQueue(PrivateQueueName);
            else
                messageQueue = MessageQueue.Create(PrivateQueueName);
            messageQueue.MessageReadPropertyFilter.CorrelationId = true;
            messageQueue.MessageReadPropertyFilter.Extension = true;
        }

        [TestMethod]
        public void Test_MsmqMessageOperations_Write_And_Read_Message_Msmq()
        {
            const string ExpectedBody = "this is the message body";
            const string ExpectedLabel = "This is the message label";
            var message = new Message();
            
            message.Body = ExpectedBody;
            message.Label = ExpectedLabel;
            message.Formatter = new BinaryMessageFormatter();
            messageQueue.Send(message);

            message = messageQueue.Receive(TimeSpan.FromSeconds(3));
            message.Formatter = new BinaryMessageFormatter();

            Assert.AreEqual(ExpectedBody, message.Body.ToString());
            Assert.AreEqual(ExpectedLabel, message.Label);
        }

        [TestMethod]
        public void Test_MsmqMessageOperations_Write_And_Read_Stream_Msmq()
        {
            byte[] array = new byte[MaxMessageSize + 1];
            var stream = new MemoryStream(array);
            byte[] buffer = new byte[MaxMessageSize];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string correlationId = null;
            string firstMesssageCorrelationId = string.Format(@"{0}\1", Guid.NewGuid().ToString());
            bool isFirstPass = true;
            do
            {                
                var message = new Message();
                message.Extension = Encoding.UTF8.GetBytes("This is the extension data.");
                message.BodyStream.Write(buffer, 0, bytesRead);
                message.Formatter = new BinaryMessageFormatter();
                message.CorrelationId = isFirstPass 
                    ? firstMesssageCorrelationId 
                    : correlationId;

                messageQueue.Send(message);
                if (correlationId == null && isFirstPass)
                    correlationId = message.Id;

                bytesRead = stream.Read(buffer, 0, buffer.Length);
                isFirstPass = false;
            }
            while (bytesRead > 0);

            // read first message
            var receiveMessage = messageQueue.ReceiveByCorrelationId(firstMesssageCorrelationId);
            Assert.IsNotNull(receiveMessage);
            var childMessage1 = messageQueue.ReceiveByCorrelationId(receiveMessage.Id);
            Assert.IsNotNull(childMessage1);
        }

        [TestMethod]
        public void Test_MsmqMessageOperations_When_Send_Interstitial_Messages_Then_ReceiveByCorrelationId_Succeeds()
        {
            var correlationId = string.Format(@"{0}\1", Guid.NewGuid().ToString());

            const string FirstMessageBody = "First Message";
            var firstMessage = new Message();
            firstMessage.Body = FirstMessageBody;
            firstMessage.CorrelationId = correlationId;
            firstMessage.Formatter = new BinaryMessageFormatter();
            messageQueue.Send(firstMessage);

            const string SecondMessageBody = "Second Message Body";
            var secondMessage = new Message();
            secondMessage.Body = SecondMessageBody;
            secondMessage.Formatter = new BinaryMessageFormatter();
            messageQueue.Send(secondMessage);

            const string ThirdMessageBody = "Third Message";
            var thirdMessage = new Message();
            thirdMessage.Body = ThirdMessageBody;
            thirdMessage.CorrelationId = correlationId;
            thirdMessage.Formatter = new BinaryMessageFormatter();
            messageQueue.Send(thirdMessage);

            var firstReceivedMessageByCorrelationId = messageQueue.ReceiveByCorrelationId(correlationId);
            firstReceivedMessageByCorrelationId.Formatter = new BinaryMessageFormatter();
            Assert.AreEqual(FirstMessageBody, firstReceivedMessageByCorrelationId.Body);
            
            var middleReceivedMessageByCorrelationId = messageQueue.ReceiveByCorrelationId(correlationId);
            middleReceivedMessageByCorrelationId.Formatter = new BinaryMessageFormatter();
            Assert.AreEqual(ThirdMessageBody, middleReceivedMessageByCorrelationId.Body);
            
            var lastReceivedMessage = messageQueue.Receive();
            lastReceivedMessage.Formatter = new BinaryMessageFormatter();
            Assert.AreEqual(SecondMessageBody, lastReceivedMessage.Body);
        }

        [ClassCleanup]
        public static void Cleanup_MsmqMessageOperations()
        {
            if (MessageQueue.Exists(PrivateQueueName))
                MessageQueue.Delete(PrivateQueueName);
        }
    }
}
