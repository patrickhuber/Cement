using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Messages
{
    public interface IMessageHandler
    {
        void HandleMessage(IMessage message);
    }
}
