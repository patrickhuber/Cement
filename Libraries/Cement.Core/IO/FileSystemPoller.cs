using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Cement.IO
{
    public class FileSystemPoller
    {
        public static readonly string DefaultPattern = "*.*";
        
        public bool EnableRaisingEvents { get; set; }
        public TimeSpan PollTimeSpan { get; set; }
        public string Path {get; set; }
        public string Pattern {get; set;}
        public bool IncludeSubDirectories{get; set; }
        public IFileSystem FileSystem { get; set; }

        public FileSystemPoller(string path, IFileSystem fileSystem)
            : this(path, DefaultPattern, fileSystem)
        {        
        }

        public FileSystemPoller(string path, string pattern, IFileSystem fileSystem)
        {
            Path = path;
            Pattern = pattern;
            FileSystem = fileSystem;
        }

        public WaitForFileResult WaitForFile()
        {
            return WaitForFile(TimeSpan.MaxValue);
        }

        public WaitForFileResult WaitForFile(TimeSpan timeout)
        {
            var started = DateTime.Now;
            while(true)
            {
                if (TimeoutElapsed(started, DateTime.Now, timeout))
                    return new WaitForFileResult(true);
                if (FileExists(Path, Pattern, IncludeSubDirectories))
                    return new WaitForFileResult(false);
                Thread.Sleep(PollTimeSpan);
            }
        }

        public async Task<WaitForFileResult> WaitForFileAsync()
        {
            return await WaitForFileAsync(TimeSpan.MaxValue);
        }

        public async Task<WaitForFileResult> WaitForFileAsync(TimeSpan timeout)
        {
            var started = DateTime.Now;
            while (true)
            {
                if (TimeoutElapsed(started, DateTime.Now, timeout))
                    return new WaitForFileResult(true);
                if (FileExists(Path, Pattern, IncludeSubDirectories))
                    return new WaitForFileResult(false);
                await Task.Delay(PollTimeSpan);
            }
        }

        private static bool TimeoutElapsed(DateTime started, DateTime now, TimeSpan timeout)
        {
            return DateTime.Now.Subtract(started) >= timeout;
        }

        private bool FileExists(string path, string pattern, bool recursive)
        {
            return null != FileSystem
                .EnumerateFiles(path, pattern, recursive)
                .FirstOrDefault();
        }
    }
}
