using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Glue.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public class Message : Glue.Adapters.IMessage
    {
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public Stream Body { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public IMessageHeader Header { get; set; }
    }
}
