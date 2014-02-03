using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glue.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutputAdapter
    {
        /// <summary>
        /// Sends the message
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="message">The message.</param>
        void Send(IAdapterContext context, IMessage message);
    }
}
