using Cement.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Cement.Build
{
    public class XsdTask : Task
    {
        public ITaskItem[] Sources { get; set; }
        public string Language { get; set; }
        
        private ICodeDomProviderFactory codeDomProviderFactory;
        private IFileSystem fileSystem;

        public XsdTask()
            : this(new CodeDomProviderFactory(), new FileSystem())
        { 
        }

        public XsdTask(ICodeDomProviderFactory codeDomProviderFactory, IFileSystem fileSystem)
        {
            this.codeDomProviderFactory = codeDomProviderFactory;
            this.fileSystem = fileSystem;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }

        private void ImportSchemas()
        {
            XmlSchemas xmlSchemas = new XmlSchemas();
            var importedSchemas = new Dictionary<string, XmlSchema>();

            foreach (var source in Sources)
            {
                string file = source.ItemSpec != null 
                    ? source.ItemSpec.Trim() 
                    : null;
                if(file != null && file.Length != 0)
                {
                    string fullPath = Path.GetFullPath(file).ToLower(CultureInfo.InvariantCulture);
                    if(!importedSchemas.ContainsKey(fullPath))
                    {
                        XmlSchema schema = ReadSchema(fullPath);
                    }
                }
            }
        }

        private XmlSchema ReadSchema(string location)
        {
            if (!fileSystem.FileExists(location))
                throw GetSchemaNotFoundExeption(location);
            var xmlTextReader = new XmlTextReader(location, new StreamReader(location).BaseStream);
            xmlTextReader.XmlResolver = (XmlResolver)null;

            bool schemaCompileErrors = false;
            XmlSchema xmlSchema = XmlSchema.Read(
                xmlTextReader, 
                new ValidationEventHandler((o, args)=>
                {
                    schemaCompileErrors = true;
                }));
            if (!schemaCompileErrors)
                return xmlSchema;
            throw new InvalidOperationException("");
        }

        private static Exception GetSchemaNotFoundExeption(string location)
        {
            return new FileNotFoundException(
                string.Format(Exceptions.SchemaNotFoundException, location));
        }
    }
}
