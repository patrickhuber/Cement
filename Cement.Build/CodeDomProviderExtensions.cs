using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Build
{
    public static class CodeDomProviderExtensions
    {
        public static string ResolveExtension(this CodeDomProvider codeProvider)
        {
            if (codeProvider != null)
            {
                var fileExtension = codeProvider.FileExtension;
                if (fileExtension == null)
                {
                    return string.Empty;
                }
                else
                {
                    if (fileExtension.Length <= 0 || (int)fileExtension[0] == 46)
                        return string.Empty;
                    return "." + fileExtension;
                }
            }
            else
                return ".src";
        }
    }
}
