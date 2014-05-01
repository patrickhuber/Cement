using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.VisualStudio.Project
{
    public class CementBuildableProjectConfig : BuildableProjectConfig
    {
        ProjectConfig config = null;
        public CementBuildableProjectConfig(ProjectConfig projectConfig)
            : base(projectConfig)
        {
            this.config = projectConfig;
        }

        override public int StartBuild(IVsOutputWindowPane pane, uint options)
        {
            config.PrepareBuild(false);

            System.Windows.Forms.MessageBox.Show("Start Build Pressed");

            NotifyBuildEnd(MSBuildResult.Successful, MsBuildTarget.Rebuild);
            return VSConstants.S_OK;
        }

        public override int StartClean(IVsOutputWindowPane pane, uint options)
        {
            config.PrepareBuild(true);

            System.Windows.Forms.MessageBox.Show("Start Build Clean Pressed");

            NotifyBuildEnd(MSBuildResult.Successful, MsBuildTarget.Clean);
            return VSConstants.S_OK;
        }

        public override int Stop(int fsync)
        {
            return VSConstants.S_OK;
        }
    }
}
