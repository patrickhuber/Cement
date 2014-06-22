using Cement.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Tests.Unit.IO
{
    [TestClass]
    public class BlockingStreamTests
    {
        [TestMethod]
        public Task Test_BlockingStream_Write_Speed_Exceeds_Read_Speed()
        {
            Assert.Fail();
            return new Task(() => { });
        }

        [TestMethod]
        public Task Test_BlockingStream_Read_Speed_Exceeds_Write_Speed()
        {
            Assert.Fail();
            return new Task(() => { });
        }
    }
}
