using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Integration.Adapters
{
    public class Message : IMessage
    {
        public Stream Body { get; set; }
        public IMessageHeader Header { get; set; }
    }
}
