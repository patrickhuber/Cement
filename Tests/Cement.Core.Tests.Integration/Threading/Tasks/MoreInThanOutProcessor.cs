using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Core.Tests.Integration.Threading.Tasks
{
    public class MoreInThanOutProcessor : Cement.Core.Tests.Integration.Threading.Tasks.IProcessor
    {
        public void Process(Stream inStream, Stream outStream)
        {
            byte[] readBuffer = new byte[2048];
            byte[] writeBuffer = new byte[1024];

            int bytesRead = 0;
            do
            {
                var count = readBuffer.Length - bytesRead;
                bytesRead = inStream.Read(readBuffer, 0, count);
                for (int i = 0; i < writeBuffer.Length; i += 2)
                {
                    writeBuffer[i] = readBuffer[i * 2];
                }
            } while (bytesRead > 0);
        }
    }
}
