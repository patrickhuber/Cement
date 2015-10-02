using System.Collections.Generic;
using System.IO;

namespace Cyrus
{
    public interface IMessage
    {
        IDictionary<string, string> Header { get; }
        Stream Body { get; }
    }
}