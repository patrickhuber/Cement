using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Build.Framework;

namespace Cement.Build.Tests.Unit
{
    [TestClass]
    public class SvcUtilTaskTests
    {
        [TestMethod]
        public void Test_SvcUtilTask_Execute()
        {
            var task = new SvcUtilTask();
            task.BuildEngine = new Mock<IBuildEngine>().Object;
            task.Execute();
        }
    }
}
