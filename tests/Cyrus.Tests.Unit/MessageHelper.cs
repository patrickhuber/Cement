using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cyrus.Tests.Unit
{
    public class MessageHelper
    {
        public static Message Create()
        {
            return Create("0123456789");
        }

        public static Message Create(string content)
        {
            return Create(content, new Dictionary<string, string> { { "prop1", "value1" } });
        }

        public static Message Create(string content, IDictionary<string, string> headers)
        {
            var body = new MemoryStream(Encoding.ASCII.GetBytes(content));
            var message = new Message(body, headers);
            return message;
        }
    }
}
