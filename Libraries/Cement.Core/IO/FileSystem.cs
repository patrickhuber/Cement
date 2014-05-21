using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.IO
{
    public class FileSystem : IFileSystem
    {
        private const string DefaultSearchPattern = "*.*";

        public Stream OpenRead(string path)
        {
            return File.OpenRead(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
        
        public Stream OpenWrite(string path)
        {
            return File.OpenWrite(path);
        }

        public string GetFirstFile(string path, string pattern, bool recursive)
        {
            return EnumerateFiles(path, pattern, recursive).FirstOrDefault();
        }

        public IEnumerable<string> EnumerateFiles(string path, bool recursive)
        {
            return EnumerateFiles(path, DefaultSearchPattern, recursive);
        }

        public IEnumerable<string> EnumerateFiles(string path, string pattern, bool recursive)
        {
            return Directory.EnumerateFiles(path, pattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}
