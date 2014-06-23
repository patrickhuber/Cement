using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Messages
{
    public class InMemoryMessageFactory : IMessageFactory
    {
        public IMessage Create()
        {
            return Create(new MessageContext());
        }
        
        public IMessage Create(IMessageContext messageContext)
        {
            return new Message(
                new MemoryStream(),
                messageContext);
        }
    }
}
