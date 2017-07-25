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
        [TestMethod]
        public void FileSystemEnumerateFilesShouldEnumerateDirectory()
        {
            var fileSystem = new FileSystem();
            var files = fileSystem.EnumerateFiles(
                Path.Combine(Directory.GetCurrentDirectory(), "IO"));
            int count = files.Count();
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void FileSystemEnumerateFilesGivenFilterShouldReturnSubsetOfFiles()
        {
            var fileSystem = new FileSystem();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "IO");
            var filter = "*.txt";
            var files = fileSystem.EnumerateFiles(
                path,
                filter);
            int count = files.Count();
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void FileSystemOpenShouldOpenFile()
        {
            var fileSystem = new FileSystem();
            using (var fileStream = fileSystem.OpenFile(
                Path.Combine(Directory.GetCurrentDirectory(), @"IO\1.txt"),
                FileMode.Open))
            {
                var streamReader = new StreamReader(fileStream);
                var contents = streamReader.ReadToEnd();
                Assert.IsTrue(string.IsNullOrWhiteSpace(contents));
            }
        }

        [TestMethod]
        public void FileSystemFileExistsShouldReturnTrueWhenFileExists()
        {
            var fileSystem = new FileSystem();
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"IO\1.txt");
            Assert.IsTrue(fileSystem.FileExists(path));
        }

        [TestMethod]
        public void FileSystemDeleteFileShouldDeleteFile()
        {
            var fileSystem = new FileSystem();
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"IO\1.txt");
            fileSystem.DeleteFile(path);
            Assert.IsFalse(fileSystem.FileExists(path));

            // cleanup
            using (fileSystem.OpenFile(path, FileMode.OpenOrCreate)) { }
            Assert.IsTrue(fileSystem.FileExists(path));
        }
    }
}
