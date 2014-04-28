using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.VisualStudio.Project
{
    public class CementProjectNode : ProjectNode
    {
        private CementProjectPackage package;

        public CementProjectNode(CementProjectPackage package)
        {
            this.package = package;
        }

        public override Guid ProjectGuid
        {
            get { return new Guid(GuidList.guidCement_VisualStudio_ProjectFactory); }
        }

        public override string ProjectType
        {
            get { return "CementProjectType"; }
        }

        public override void AddFileFromTemplate(string source, string target)
        {
            this.FileTemplateProcessor.UntokenFile(source, target);
            this.FileTemplateProcessor.Reset();
        }

        protected override ConfigProvider CreateConfigProvider()
        {
            return new CementConfigProvider(this);
        }

        public virtual CementProjectConfig MakeConfiguration(string activeConfigName)
        {
            return new CementProjectConfig(this, activeConfigName);
        }

        public IProjectLauncher GetLauncher()        
        {
            return new CementProjectLauncher();
        }
    }
}
