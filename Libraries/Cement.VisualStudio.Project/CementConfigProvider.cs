using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cement.VisualStudio.Project
{
    /// <summary>
    /// http://social.msdn.microsoft.com/Forums/en-US/9f5d3410-ac7c-43df-b7ab-8be6114d1735/vs-shell-question-about-extending-visual-studio-vspackage-using-mpfproj?forum=vsx
    /// </summary>
    [ComVisible(true)]
    [CLSCompliant(false)]
    public class CementConfigProvider : ConfigProvider
    {        
        private CementProjectNode _project;
        private Dictionary<string,ProjectConfig> _configurationsList;

        public CementConfigProvider(CementProjectNode project)
            : base(project)
        {
            _project = project;
            _configurationsList = new Dictionary<string, ProjectConfig>();
        }

        #region overridden methods

        protected override ProjectConfig CreateProjectConfiguration(string configName)
        {
            if (_configurationsList.ContainsKey(configName))
                return _configurationsList[configName];
            var projectConfig = new CementProjectConfig(_project, configName);
            _configurationsList.Add(configName, projectConfig);
            return projectConfig;
        }

        public override int GetPlatformNames(uint celt, string[] names, uint[] actual)
        {
            if (names != null)
            {
                names[0] = "Any CPU";
            }

            if (actual != null)
            {
                actual[0] = 1;
            }

            return VSConstants.S_OK;
        }

        public override int GetSupportedPlatformNames(uint celt, string[] names, uint[] actual)
        {
            if (names != null)
            {
                names[0] = "Any CPU";
            }

            if (actual != null)
            {
                actual[0] = 1;
            }

            return VSConstants.S_OK;
        }
        #endregion
    }
}
