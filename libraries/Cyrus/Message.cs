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
            MessageHeader = properties;
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
        
        public void Dispose()
        {
            Body.Dispose();
        }

        public static async Task CopyToAsync(IMessageReader message, Stream stream)
        {
            var buffer = new byte[1024];
            var bytesRead = 0;

            do
            {
                bytesRead = await message.ReadAsync(buffer, 0, buffer.Length);
                await stream.WriteAsync(buffer, 0, bytesRead);
            } while (bytesRead > 0);
        }

        public static void CopyTo(IMessageReader message, Stream stream)
        {
            var buffer = new byte[1024];
            var bytesRead = 0;

            do
            {
                bytesRead = message.Read(buffer, 0, buffer.Length);
                stream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);
        }

        public static async Task CopyFromAsync(Stream stream, IMessageWriter message)
        {
            var buffer = new byte[1024];
            var bytesRead = 0;

            do
            {
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                await message.WriteAsync(buffer, 0, bytesRead);
            } while (bytesRead > 0);
        }

        public static void CopyFrom(Stream stream, IMessageWriter message)
        {
            var buffer = new byte[1024];
            var bytesRead = 0;

            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                message.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);
        }
    }
}
