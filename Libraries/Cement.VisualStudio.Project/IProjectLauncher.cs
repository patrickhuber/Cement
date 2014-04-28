using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.VisualStudio.Project
{
    public interface IProjectLauncher
    {
        /// <summary>
        /// Starts a project with or without debugging.
        /// 
        /// Returns an HRESULT indicating success or failure.
        /// </summary>
        int LaunchProject(bool debug);

        /// <summary>
        /// Starts a file in a project with or without debugging.
        /// 
        /// Returns an HRESULT indicating success or failure.
        /// </summary>
        int LaunchFile(string file, bool debug);
    }
}
