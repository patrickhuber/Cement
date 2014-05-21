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
            return new Message(
                new MemoryStream(),
                new MessageContext());
        }
    }
}
