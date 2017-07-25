using System;
using System.Collections.Generic;
using System.IO;

namespace Cyrus.IO
{
    public interface IFileSystem
    {
        void DeleteFile(string path);
        IEnumerable<string> EnumerateFiles(string path);
        IEnumerable<string> EnumerateFiles(string path, string filter);
        bool FileExists(string path);
        void MoveFile(string fromPath, string toPath);
        Stream OpenFile(string path, FileMode fileMode);
        DateTime GetFileCreationTimeUtc(string path);
        DateTime GetFileLastWriteTimeUtc(string path);
    }
}