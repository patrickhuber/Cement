using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cyrus.File;
using System.Linq;
using System.IO;
using Cyrus.File.Tests.Integration;

namespace Cyrus.Tests.Unit
{
    /// <summary>
    /// Summary description for FileSystemTests
    /// </summary>
    [TestClass]
    public class FileSystemTests
    {

        [ClassInitialize]
        public static void InitializeFileSystemTests(TestContext testContext)
        {
            DeploymentHelper.Deploy("1.txt", "folder");
            DeploymentHelper.Deploy("2.txt", "folder");
            DeploymentHelper.Deploy("3.dat", "folder");
        }

        private static string GetStagingDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "folder");
        }

        [TestMethod]
        public void FileSystemEnumerateFilesShouldEnumerateDirectory()
        {
            var fileSystem = new FileSystem();
            string path = GetStagingDirectory();
            var files = fileSystem.EnumerateFiles(path);
            int count = files.Count();
            Assert.AreEqual(3, count);
        }


        [TestMethod]
        public void FileSystemEnumerateFilesGivenFilterShouldReturnSubsetOfFiles()
        {
            var fileSystem = new FileSystem();
            var path = GetStagingDirectory();
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
                Path.Combine(GetStagingDirectory(), @"1.txt"),
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
            var path = Path.Combine(GetStagingDirectory(), @"1.txt");
            Assert.IsTrue(fileSystem.FileExists(path));
        }

        [TestMethod]
        public void FileSystemDeleteFileShouldDeleteFile()
        {
            var fileSystem = new FileSystem();
            var path = Path.Combine(GetStagingDirectory(), @"1.txt");
            fileSystem.DeleteFile(path);
            Assert.IsFalse(fileSystem.FileExists(path));

            // cleanup
            using (fileSystem.OpenFile(path, FileMode.OpenOrCreate)) { }
            Assert.IsTrue(fileSystem.FileExists(path));
        }
    }
}
