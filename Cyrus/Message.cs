using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cyrus
{
    public class Message : IMessageReader, IMessageWriter
    {
        public IDictionary<string, string> MessageHeader { get; private set; }
        public Stream Body { get; private set; }

        public Message(Stream body, IDictionary<string, string> properties)
        {
            MessageHeader = new Dictionary<string, string>();
            Body = body;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return Body.Read(buffer, offset, count);
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            return await Body.ReadAsync(buffer, offset, count);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            Body.Write(buffer, offset, count);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count)
        {
            await Body.WriteAsync(buffer, offset, count);
        }

        public void Close()
        {
            Body.Close();
        }

        public void Dispose()
        {
            Body.Dispose();
        }
    }
}
