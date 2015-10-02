using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cyrus.IO;
using System.Linq;
using System.IO;

namespace Cyrus.Tests.Unit
{
    /// <summary>
    /// Summary description for FileSystemTests
    /// </summary>
    [TestClass]
    [DeploymentItem(@"IO\1.txt", "IO")]
    [DeploymentItem(@"IO\2.txt", "IO")]
    [DeploymentItem(@"IO\3.dat", "IO")]
    public class FileSystemTests
    {  
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get; set;
        }
        
        [TestMethod]
        public void Test_FileSystem_That_Enumerates_Directory()
        {
            var fileSystem = new FileSystem();
            var files = fileSystem.EnumerateFiles(
                Path.Combine(TestContext.DeploymentDirectory, "IO"));
            int count = files.Count();
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void Test_FileSystem_That_Filter_Enumerates_Only_Applicable_Files()
        {
            var fileSystem = new FileSystem();
            var path = Path.Combine(TestContext.DeploymentDirectory, "IO");
            var filter = "*.txt";
            var files = fileSystem.EnumerateFiles(
                path,
                filter);
            int count = files.Count();
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void Test_FileSystem_That_Can_Open_File()
        {
            var fileSystem = new FileSystem();
            using (var fileStream = fileSystem.OpenFile(
                Path.Combine(TestContext.DeploymentDirectory, @"IO\1.txt"),
                FileMode.Open))
            {
                var streamReader = new StreamReader(fileStream);
                var contents = streamReader.ReadToEnd();
                Assert.IsTrue(string.IsNullOrWhiteSpace(contents));
            }            
        }

        [TestMethod]
        public void Test_FileSystem_That_File_Exists_Returns_True_When_File_Exists()
        {
            var fileSystem = new FileSystem();
            var path = Path.Combine(TestContext.DeploymentDirectory, @"IO\1.txt");
            Assert.IsTrue(fileSystem.FileExists(path));
        }

        [TestMethod]
        public void Test_FileSystem_That_File_Delete_Removes_File()
        {
            var fileSystem = new FileSystem();
            var path = Path.Combine(TestContext.DeploymentDirectory, @"IO\1.txt");
            fileSystem.DeleteFile(path);
            Assert.IsFalse(fileSystem.FileExists(path));

            // cleanup
            using(fileSystem.OpenFile(path, FileMode.OpenOrCreate)) {; }
            Assert.IsTrue(fileSystem.FileExists(path));
        }


    }
}
