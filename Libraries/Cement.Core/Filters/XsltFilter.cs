using Cement.Channels;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Cement.Filters
{
    public class XsltFilter : IFilter
    {
        private XslCompiledTransform transform;
        private IMessageFactory messageFactory;

        public XsltFilter(
            XslCompiledTransform transform, 
            IMessageFactory messageFactory,
            IChannel outChannel)
        {
            this.transform = transform;
            this.messageFactory = messageFactory;
            this.OutChannel = outChannel;
        }

        public IChannel OutChannel
        {
            get;
            private set;
        }

        public void HandleMessage(IMessage message)
        {
            var outMessage = messageFactory.Create(message.Context);
            OutChannel.Publish(outMessage);
            outMessage.Body.Close();
        }
    }
}
