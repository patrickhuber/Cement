using Cement.VisualStudio.Project;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cement.VisualStudio.Project
{
    [Guid(GuidList.guidCement_VisualStudio_ProjectFactory)]
    public class CementProjectFactory : ProjectFactory
    {
        protected override ProjectNode CreateProject()
        {
            throw new NotImplementedException();
        }
    }
}
