using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace Cement.VisualStudio.Project
{
    [ComVisible(true)]
    public class CementProjectConfig : ProjectConfig
    {
        private readonly CementProjectNode/*!*/ _project;

        public CementProjectConfig(CementProjectNode/*!*/ project, string configuration)
            : base(project, configuration)
        {
            _project = project;
        }

        public override int DebugLaunch(uint flags)
        {
            IProjectLauncher starter = _project.GetLauncher();

            __VSDBGLAUNCHFLAGS launchFlags = (__VSDBGLAUNCHFLAGS)flags;
            if ((launchFlags & __VSDBGLAUNCHFLAGS.DBGLAUNCH_NoDebug) == __VSDBGLAUNCHFLAGS.DBGLAUNCH_NoDebug)
            {
                //Start project with no debugger
                return starter.LaunchProject(false);
            }
            else
            {
                //Start project with debugger 
                return starter.LaunchProject(true);
            }
        }
    }
}
