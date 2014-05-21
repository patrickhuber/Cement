using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.Messages
{
    public interface IMessageContext : IContext
    {
        void CopyTo(IMessageContext messageContext);
    }
}
