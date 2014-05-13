using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cement.Adapters;
using Moq;
using Cement.IO;
using System.IO;

namespace Cement.Tests.Unit
{
    /// <summary>
    /// Summary description for FileSystemChannelTests
    /// </summary>
    [TestClass]
    public class FileSystemReceiveAdapterTests
    {
        public FileSystemReceiveAdapterTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_FileSystemChannel_Receive()
        {
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x=>x.OpenRead(It.IsAny<string>()))
                .Returns<string>(s=> 
                {
                    var bytes = Encoding.ASCII.GetBytes("this is my message.");
                    return new MemoryStream(bytes);
                });
            var mockChannelContext = new Mock<IAdapterContext>();
            mockChannelContext
                .SetupGet(x => x.Attributes)
                .Returns(new Dictionary<string, string>
                {
                    {Cement.Adapters.AdapterProperties.Uri, "file:///c:/users/testuser/documents/myfile.txt"}
                });
            var fileSystemChannel = new FileSystemReceiveAdapter(
                mockChannelContext.Object, 
                mockFileSystem.Object);
            
            var message = fileSystemChannel.Receive();
            Assert.IsNotNull(message);
            Assert.IsNotNull(message.Context);
            Assert.IsNotNull(message.Body);
        }
    }
}
