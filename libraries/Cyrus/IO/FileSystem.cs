using System;
using System.Collections.Generic;
using System.IO;

namespace Cyrus.IO
{
    public class FileSystem : IFileSystem
    {
        public IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public IEnumerable<string> EnumerateFiles(string path, string filter)
        {
            return Directory.EnumerateFiles(path, filter);
        }
        
        public Stream OpenFile(string path, FileMode fileMode)
        {
            return File.Open(path, fileMode);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);            
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void MoveFile(string fromPath, string toPath)
        {
            File.Move(fromPath, toPath);
        }

        public DateTime GetFileCreationTimeUtc(string path)
        {
            return File.GetCreationTimeUtc(path);
        }

        public DateTime GetFileLastWriteTimeUtc(string path)
        {
            return File.GetLastWriteTimeUtc(path);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public Stream CreateFile(string path)
        {
            return File.Create(path);
        }
    }
}
