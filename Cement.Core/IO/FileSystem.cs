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
        public Stream OpenRead(string path)
        {
            return File.OpenRead(path);
        }

        public string GetFirstFile(string path, string pattern, bool recursive)
        {
            return Directory
                    .EnumerateFiles(path, pattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .FirstOrDefault();
        }
    }
}
