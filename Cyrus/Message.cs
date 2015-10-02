using System.Collections.Generic;
using System.IO;

namespace Cyrus
{
    public class Message : IMessage
    {
        public Stream Body { get; private set; }
        public IDictionary<string, string> Header { get; private set; }

        public Message(Stream body, IDictionary<string, string> header)
        {
            Body = body;
            Header = header;
        }
    }
}
