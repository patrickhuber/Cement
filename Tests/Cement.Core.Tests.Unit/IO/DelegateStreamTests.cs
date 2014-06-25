using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cement.IO;
using System.IO;
using System.Text;
using System.Linq;

namespace Cement.Tests.Unit.IO
{
    [TestClass]
    public class DelegateStreamTests
    {
        [TestMethod]
        public void Test_DelegateStream_Push()
        {
            var array = Enumerable.Repeat<byte>(0x00, 1024).ToArray();
            var memoryStream = new MemoryStream(array);
            DelegateStream delegateStream1 = new DelegateStream(
                memoryStream.Read, 
                (b, o, c) => { });
        }
    }
}
