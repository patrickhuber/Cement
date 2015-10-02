using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileReceiveMessageProperties
    {
        public static readonly string Namespace = "Cyrus.IO.File.Receive";
        public static readonly string Path = Namespace + ".Path";
        public static readonly string Directory = Namespace + ".Directory";
        public static readonly string CreatedUtc = Namespace + ".CreatedTmeUtc";
        public static readonly string LastWriteTimeUtc = Namespace + ".LastWriteTimeUtc";
        public static readonly string Filter = Namespace + ".Filter";
    }
}
