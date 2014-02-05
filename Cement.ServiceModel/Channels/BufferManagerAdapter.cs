using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public class BufferManagerAdapter : IBufferManager
    {
        private BufferManager bufferManager;

        public BufferManagerAdapter(BufferManager bufferManager)
        {
            this.bufferManager = bufferManager;
        }

        public byte[] TakeBuffer(int bufferSize)
        {
            return bufferManager.TakeBuffer(bufferSize);
        }

        public void ReturnBuffer(byte[] buffer)
        {
            bufferManager.ReturnBuffer(buffer);
        }
    }
}
