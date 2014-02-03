using System;
namespace Glue.Adapters
{
    public interface IMessage
    {
        System.IO.Stream Body { get; set; }
        IMessageHeader Header { get; set; }
    }
}
