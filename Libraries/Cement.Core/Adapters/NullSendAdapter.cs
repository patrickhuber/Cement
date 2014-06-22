using Cement.Channels;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Adapters
{
    public class NullSendAdapter : ISendAdapter
    {
        public IChannel InChannel
        {
            get { throw new NotImplementedException(); }
        }
    }
}
