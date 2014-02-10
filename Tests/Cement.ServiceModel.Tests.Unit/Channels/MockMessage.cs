using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Cement.ServiceModel.Tests.Unit.Channels
{
    public class MockMessage : Message
    {
        private MessageHeaders messageHeaders;
        private MessageProperties messageProperties;
        private MessageVersion messageVersion;

        public override MessageHeaders Headers
        {
            get { return messageHeaders; }
        }

        protected override void OnWriteBodyContents(System.Xml.XmlDictionaryWriter writer)
        {
            
        }

        public override MessageProperties Properties
        {
            get { return messageProperties; }
        }

        public override MessageVersion Version
        {
            get { return messageVersion; }
        }
    }
}
