using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using Cement.IO;

namespace Cement.Tests.Unit.Filters
{
    [TestClass]
    public class XsltCompiledTransformTests
    {
        XDocument xslDocument;

        [TestInitialize]
        public void Initialize_XslCompiledTransform_Tests()
        {
            xslDocument = XDocument.Parse(
            @"<?xml version=""1.0"" encoding=""utf-8""?>
                <xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
                    <xsl:output method=""xml"" indent=""yes""/>

                    <xsl:template match=""@* | node()"">
                        <xsl:copy>
                            <xsl:apply-templates select=""@* | node()""/>
                        </xsl:copy>
                    </xsl:template>
                </xsl:stylesheet>");
        }

        [TestMethod]
        public void Test_XsltCompiledTranform_Reads_Observable()
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            
            transform.Load(xslDocument.CreateReader());            
        }

        [TestMethod]
        public void Test_XsltCompiledTransform_Pipelining()
        {            
            var xsltTransform = new XslCompiledTransform();
            xsltTransform.Load(xslDocument.CreateReader());
            
            string xmlInput = @"<nodes><node></node></nodes>";
            var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(xmlInput));
            var xPathDocument = new XPathDocument(inputStream);

            var outputStream = new MemoryStream();
            var xsltArguments = new XsltArgumentList();
            
            xsltTransform.Transform(
                xPathDocument, 
                xsltArguments, 
                outputStream);
            outputStream.Flush();
            
            var result = outputStream.ToArray();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void Test_XsltCompiledTransform_DelegateStream()
        {
            var xsltTransform = new XslCompiledTransform();
            xsltTransform.Load(xslDocument.CreateReader());

            string xmlInput = @"<nodes><node></node></nodes>";
            var inputStream = new StringReader(xmlInput);
            var xPathDocument = new XPathDocument(inputStream);

            var bufferQueue = new Queue<ArraySegment<byte>>();
            var outputStream = new DelegateStream(
                null, 
                (buffer, offset, count) => 
                {
                    bufferQueue.Enqueue(
                        new ArraySegment<byte>(buffer, offset, count));
                });
            var xsltArguments = new XsltArgumentList();

            xsltTransform.Transform(
                xPathDocument,
                xsltArguments,
                outputStream);
            outputStream.Flush();

            Assert.IsTrue(bufferQueue.Count > 0);
            
            var outputMemoryStream = new MemoryStream();
            
            while (bufferQueue.Count > 0)
            {
                var buffer = bufferQueue.Dequeue();                
                outputMemoryStream.Write(buffer.Array, buffer.Offset, buffer.Count);
            }
            outputMemoryStream.Seek(0, SeekOrigin.Begin);
            var result = outputMemoryStream.ToArray();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        public void Test_XsltCompiledTransform_Async_Await_Delegates()
        {
        }
    }
}
