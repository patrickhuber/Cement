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
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Cement.VisualStudio.Project
{
    [Guid(GuidList.guidCement_VisualStudio_ProjectFactory)]
    public class CementProjectFactory : ProjectFactory
    {
        private CementProjectPackage package;
        public CementProjectFactory(CementProjectPackage package) 
            : base(package)
        {
            this.package = package;
        }

        protected override ProjectNode CreateProject()
        {
            var project = new CementProjectNode(this.package);
            project.SetSite((IOleServiceProvider)((IServiceProvider)this.package).GetService(typeof(IOleServiceProvider)));            
            return project;
        }
    }
}
