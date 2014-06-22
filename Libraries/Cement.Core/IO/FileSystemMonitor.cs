using Cement.IO;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Adapters
{
    public class FileSystemMonitor : Monitor<string>
    {
        public static readonly string DefaultPattern = "*.*";

        private TimeSpan interval;
        private TimeSpan timeout;

        private string pattern;
        private string path;
        private bool recursive;
        private IFileSystem fileSystem;

        public FileSystemMonitor(string path, string pattern, TimeSpan interval, IFileSystem fileSystem)
        {
            this.path = path;
            this.pattern = pattern;
            this.fileSystem = fileSystem;
            this.interval = interval;
            this.recursive = false;
            this.timeout = TimeSpan.MaxValue;
        }
        
        public async Task StartAsync()
        {
            var started = DateTime.Now;
            try
            {
                while (true)
                {
                    if (FileExists())
                        foreach (var file in EnumerateFiles())
                            Publish(file);
                    if (TimeoutElapsed(started, DateTime.Now, timeout))
                        Error(new TimeoutException("Timout occured"));
                    await Task.Delay(interval);
                }
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        private void Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        private bool FileExists()
        {
            return null != EnumerateFiles()
                .FirstOrDefault();
        }

        private IEnumerable<string> EnumerateFiles()
        {
            return fileSystem
                .EnumerateFiles(path, pattern, recursive);
        }

        private static bool TimeoutElapsed(DateTime started, DateTime now, TimeSpan timeout)
        {
            return DateTime.Now.Subtract(started) >= timeout;
        }
    }
}
