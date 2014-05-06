using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.IO
{
    public interface IFileSystem
    {
        string GetFirstFile(string path, string pattern, bool recursive);
        Stream OpenRead(string path);
        bool FileExists(string path);
        Stream OpenWrite(string path);
    }
}
