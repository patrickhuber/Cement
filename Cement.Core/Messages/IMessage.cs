using System;
using System.IO;
namespace Cement.Messages
{
    public interface IMessage : IDisposable
    {
        Stream Body { get; set; }
        IMessageContext Header { get; set; }
    }
}
