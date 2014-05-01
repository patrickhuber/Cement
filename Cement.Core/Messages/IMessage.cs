using System;
namespace Cement.Messages
{
    public interface IMessage
    {
        System.IO.Stream Body { get; set; }
        IMessageContext Header { get; set; }
    }
}
