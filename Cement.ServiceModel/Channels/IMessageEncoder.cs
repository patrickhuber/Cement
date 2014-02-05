using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public interface IMessageEncoder
    {
        Message ReadMessage(ArraySegment<byte> arraySegment, IBufferManager bufferManager);
    }
}
