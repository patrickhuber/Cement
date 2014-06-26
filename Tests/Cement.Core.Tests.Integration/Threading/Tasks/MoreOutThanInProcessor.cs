using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Core.Tests.Integration.Threading.Tasks
{
    public class MoreOutThanInProcessor
    {
        int multiplier;

        public MoreOutThanInProcessor(int multiplier)
        {
            this.multiplier = multiplier;
        }

        public void Process(Stream inStream, Stream outStream)
        {
            byte[] buffer = new byte[1024];
            byte[] writeBuffer = new byte[2048];

            int bytesRead = 0;
            do
            {
                var count = buffer.Length - bytesRead;
                bytesRead = inStream.Read(buffer, 0, count);
                for(int i=0; i < writeBuffer.Length; i+=2)
                {
                    writeBuffer[i] = buffer[i / 2];
                    writeBuffer[i + 1] = buffer[i / 2];
                }
            } while (bytesRead > 0);
        }
    }
}
