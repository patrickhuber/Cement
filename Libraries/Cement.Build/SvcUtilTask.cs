using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;
using Microsoft.Win32;
using System.IO;
using System.Linq;

namespace Cement.Build
{
    public class SvcUtilTask : ToolTask
    {
        public bool DataContractsOnly { get; set; }
        
        public Serializer Serializer { get; set; }
        
        public string Language { get; set; }
        
        public ITaskItem[] Namespaces { get; set; }

        public bool EnableDataBinding { get; set; }

        public string Config { get; set; }

        public bool NoConfig { get; set; }
        
        public bool MergeConfig { get; set; }
        
        public ITaskItem[] Sources { get; set; }

        public ITaskItem Directory { get; set; }

        public string PathToExecutable { get; set; }
                
        protected override string GenerateFullPathToTool()
        {
            if (!string.IsNullOrWhiteSpace(PathToExecutable))
                return PathToExecutable;

            var sdkInstallPath = Registry
                .LocalMachine
                .OpenSubKey(@"Software\Microsoft\Microsoft SDKs\Windows")
                .GetValue("CurrentInstallFolder")
                .ToString();
            var sdkInstallBinPath = Path.Combine(sdkInstallPath, "bin");
            return sdkInstallBinPath;
        }

        protected override string ToolName
        {
            get { return "svcutil.exe"; }
        }

        protected override string GenerateCommandLineCommands()
        {
            var builder = new CommandLineBuilder();
            SetDefaults(builder);
            SetConfig(builder);
            SetSerializer(builder);
            SetLanguage(builder);
            SetDirectory(builder);
            return builder.ToString();
        }

        private void SetDefaults(CommandLineBuilder builder)
        {
            builder.AppendSwitch("/nologo");
            builder.AppendSwitchIfNotNull("/target:", "code");
        }

        private void SetDirectory(CommandLineBuilder builder)
        {
            ITaskItem source = null;
            // if the directory is set, use it
            if (Directory != null)
            {
                source = Directory;
            }
            else 
            {
                var firstSource = Sources.FirstOrDefault();
                if (firstSource == null)
                { 
                    Log.LogError("No sources provided. Unable to set current directory.");
                    return;
                }
                source = firstSource;
            }
            builder.AppendSwitchIfNotNull("/directory:", source.ItemSpec);
        }

        private void SetLanguage(CommandLineBuilder builder)
        {
            string language = string.Empty;
            if (!string.IsNullOrWhiteSpace(Language))
                language = Language;
            else
                language = "csharp";
            builder.AppendSwitchIfNotNull("/language:", language);
        }

        private void SetSerializer(CommandLineBuilder builder)
        {
            switch (Serializer)
            {
                case Build.Serializer.Auto:
                    builder.AppendSwitchIfNotNull("/serializer:", "Auto");
                    break;
                case Build.Serializer.DataContractSerializer:
                    builder.AppendSwitchIfNotNull("/serializer:", "DataContractSerializer");
                    break;
                case Build.Serializer.XmlSerializer:
                    builder.AppendSwitchIfNotNull("/serializer:", "XmlSerializer");
                    break;
            }
        }

        private void SetConfig(CommandLineBuilder builder)
        {
            if (NoConfig)
                builder.AppendSwitch("/noConfig");
            if (MergeConfig)
                builder.AppendSwitch("/mergeConfig");
            if (!string.IsNullOrWhiteSpace(Config))
                builder.AppendSwitchIfNotNull("/config:", Config);
        }
    }

    public enum Serializer
    {
        Auto ,
        DataContractSerializer,
        XmlSerializer
    }
}
