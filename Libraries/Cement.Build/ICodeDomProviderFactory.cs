using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Build
{
    public interface ICodeDomProviderFactory
    {
        CodeDomProvider CreateCodeDomProvider(string language);
    }
}
