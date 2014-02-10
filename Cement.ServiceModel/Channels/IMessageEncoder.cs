using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public interface IMessageEncoder
    {
        Message ReadMessage(ArraySegment<byte> arraySegment, BufferManager bufferManager);
        Message ReadMessage(Stream stream, int maxSizeOfHeaders);
    }
}
