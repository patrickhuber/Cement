using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cyrus.File.Tests.Integration
{
    public class DeploymentHelper
    {
        public static void Deploy(string file, string target)
        {
            var path = Directory.GetCurrentDirectory();
            var source = Path.Combine(path, file);

            var targetDirectory = Path.Combine(path, target);
            Directory.CreateDirectory(targetDirectory);
            var targetFile = Path.Combine(targetDirectory, file);
            System.IO.File.Copy(source, targetFile, true);
        }
    }
}
