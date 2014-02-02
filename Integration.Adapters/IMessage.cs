using System;
namespace Referee.Adapters
{
    public interface IMessage
    {
        System.IO.Stream Body { get; set; }
        IMessageHeader Header { get; set; }
    }
}
