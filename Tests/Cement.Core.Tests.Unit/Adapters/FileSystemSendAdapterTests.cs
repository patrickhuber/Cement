using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cement.IO;
using Moq;
using System.IO;
using Cement.Adapters;
using Cement.Messages;

namespace Cement.Tests.Unit.Adapters
{
    /// <summary>
    /// Summary description for FileSystemOutputChannelTests
    /// </summary>
    [TestClass]
    public class FileSystemSendAdapterTests
    {
        public FileSystemSendAdapterTests()
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
        public void Test_FileSystemChannel_Send()
        {
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.OpenWrite(It.IsAny<string>()))
                .Returns<string>(s =>
                {
                    return new MemoryStream();
                });
            var mockChannelContext = new Mock<IAdapterContext>();
            mockChannelContext
                .SetupGet(x => x.Attributes)
                .Returns(new Dictionary<string, string> 
                { 
                    {Cement.Adapters.AdapterProperties.Uri, "file:///c:/users/testuser/documents/{330A2211-4E1C-4CA4-8ED7-FAE84C1E29A9}.txt"}
                });
            var fileSystemSendAdapter = new FileSystemSendAdapter(mockChannelContext.Object, mockFileSystem.Object);
            var messageFactory = new InMemoryMessageFactory();
            using (var message = messageFactory.Create())
            {
                var stream = new MemoryStream(Encoding.UTF8.GetBytes("this is the message body"));
                stream.CopyTo(message.Body);                
                fileSystemSendAdapter.Send(message);
            }
        }
    }
}
