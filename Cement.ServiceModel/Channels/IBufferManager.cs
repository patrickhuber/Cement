using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public interface IBufferManager
    {
        byte[] TakeBuffer(int bufferSize);
        void ReturnBuffer(byte[] buffer);
    }
}
