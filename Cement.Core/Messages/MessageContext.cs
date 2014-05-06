using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Messages
{
    public class MessageContext : IMessageContext
    {
        private IDictionary<string, string> attributes;

        public MessageContext()
        {
            attributes = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Attributes
        {
            get { return attributes; }
        }
    }
}
