using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.VisualStudio.Project
{
    /// <summary>
    /// http://social.msdn.microsoft.com/Forums/en-US/9f5d3410-ac7c-43df-b7ab-8be6114d1735/vs-shell-question-about-extending-visual-studio-vspackage-using-mpfproj?forum=vsx
    /// </summary>
    public class CementConfigProvider : ConfigProvider
    {        
        private CementProjectNode _project;

        public CementConfigProvider(CementProjectNode project)
            : base(project)
        {
            _project = project;
        }

        #region overridden methods

        protected override ProjectConfig CreateProjectConfiguration(string configName)
        {
            return _project.MakeConfiguration(configName);
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
