using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Referee.Adapters
{
    /// <summary>
    /// Adapter processes a particular protocol to produce a stream of bytes.
    /// </summary>
    public interface IInputAdapter
    {
        /// <summary>
        /// Receives a message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IMessage Receive(IAdapterContext context);
    }
}
