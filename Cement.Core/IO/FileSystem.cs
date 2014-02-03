using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cement.IO
{
    public class FileSystem : IFileSystem
    {
        public string GetFirstFile(string path, string pattern, bool recursive)
        {
            throw new NotImplementedException();
        }
        
        public Stream OpenRead(string path)
        {
            throw new NotImplementedException();
        }
    }
}
