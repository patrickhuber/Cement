using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.ServiceModel.Channels
{
    public interface IMessageWriter
    {
        void WriteBuffered();
        void WriteStreamed();
    }
}
