using Cement.Adapters;
using Cement.Channels;
using Cement.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
            var message = receiveAdapter.Receive();
            var passThroughChannel = new PassThroughChannel();
            passThroughChannel.Subscribe(receiveAdapter);
        }
    }
}
