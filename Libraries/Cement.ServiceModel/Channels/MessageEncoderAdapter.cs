using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Cement.ServiceModel.Channels
{
    public class MessageEncoderAdapter : IMessageEncoder
    {
        private MessageEncoder messageEncoder;

        public MessageEncoderAdapter(MessageEncoder messageEncoder)
        {
            this.messageEncoder = messageEncoder;
        }

        public Message ReadMessage(ArraySegment<byte> arraySegment, BufferManager bufferManager)
        {
            return this.messageEncoder.ReadMessage(arraySegment, bufferManager);
        }

        public Message ReadMessage(Stream stream, int maxSizeOfHeaders)
        {
            return this.messageEncoder.ReadMessage(stream, maxSizeOfHeaders);
        }
    }
}
