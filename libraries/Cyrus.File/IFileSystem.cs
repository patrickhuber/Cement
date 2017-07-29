using System;
using System.Collections.Generic;
using System.IO;

namespace Cyrus.File
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
        string GetFileName(string path);

        /// <summary>
        /// Creates or overwrites a file in the specified path.
        /// </summary>
        /// <param name="path">The path and name of the file to create. </param>
        /// <returns>A stream that provides read/write access to the file specified in path</returns>
        Stream CreateFile(string path);
    }
}