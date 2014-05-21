using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Build
{
    public class CodeDomProviderFactory : ICodeDomProviderFactory
    {
        public System.CodeDom.Compiler.CodeDomProvider CreateCodeDomProvider(string language)
        {
            CodeDomProvider provider;
            if (CodeDomProvider.IsDefinedLanguage(language))
                provider = CreateCodeDomProviderFromLanguage(language);
            else
                provider = CreateCodeDomProviderFromType(language);

            return provider;
        }

        private CodeDomProvider CreateCodeDomProviderFromType(string language)
        {
            Type type = Type.GetType(language, false, true);
            if (type == (Type)null)
                throw GetInvalidLanguageException(language);
            object instance = Activator.CreateInstance(type);
            if (instance is CodeDomProvider)
                return instance as CodeDomProvider;
            throw GetInvalidLanguageException(language);
        }

        private CodeDomProvider CreateCodeDomProviderFromLanguage(string language)
        {
            try
            {
                return CodeDomProvider.CreateProvider(language);
            }
            catch (Exception ex)
            {
                throw GetInvalidLanguageException(language, ex);
            }
        }

        private Exception GetInvalidLanguageException(string language)
        {
            return new InvalidOperationException(
                string.Format(Exceptions.InvalidLanguageException, language));
        }

        private Exception GetInvalidLanguageException(string language, Exception innerException)
        {
            return new InvalidOperationException(
                string.Format(Exceptions.InvalidLanguageException, language), innerException);
        }
    }
}
