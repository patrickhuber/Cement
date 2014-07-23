using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Core.Tests.Integration.Threading.Tasks
{
    public class EchoProcessor : IProcessor
    {
        public void Process(System.IO.Stream inStream, System.IO.Stream outStream)
        {
            inStream.CopyTo(outStream);
        }
    }
}
