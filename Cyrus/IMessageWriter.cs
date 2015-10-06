using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cyrus
{
    public interface IMessageWriter : IDisposable
    {
        IDictionary<string, string> MessageHeader { get; }
        void Write(byte[] buffer, int offset, int count);
        Task WriteAsync(byte[] buffer, int offset, int count);
        void Close();
    }
}
