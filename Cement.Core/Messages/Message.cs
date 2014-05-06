using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Messages
{
    public class Message : IMessage
    {
        public System.IO.Stream Body { get; set; }

        public IMessageContext Header { get; set; }

        public void Dispose()
        {
            Body.Dispose();
        }
    }
}
