using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.File
{
    public static class FileReceiveProperties
    {
        public static readonly string Namespace = "Cyrus.File.ReceivedFile.";
        public static readonly string CreationTimeUtc = Namespace + "CreationTimeUtc";
        public static readonly string LastWriteTimeUtc = Namespace + "LastModifiedTimeUtc";
        public static readonly string Path = Namespace + "Path";
        public static readonly string SourceFileName = Namespace + "SourceFileName";
        public static readonly string Filter = Namespace + "Filter";
    }
}
