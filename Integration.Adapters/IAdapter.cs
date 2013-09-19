using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Integration.Adapters
{
    /// <summary>
    /// Adapter processes a particular protocol to produce a stream of bytes.
    /// </summary>
    public interface IAdapter
    {
        IMessage Process(IAdapterContext context);
    }
}
