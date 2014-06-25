using Cement.IO;
using Cement.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Cement.Transforms.Tests.Unit
{
    [TestClass]
    public class XslCompiledTransformTests
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
        public void Test_XslCompiledTransform_Pipelining()
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
        public void Test_XslCompiledTransform_DelegateStream()
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
               

        private class QueueWriter 
        {
            Queue<byte[]> queue;
            public QueueWriter(Queue<byte[]> queue)
            {
                queue = new Queue<byte[]>();
            }

            public void Start(Stream stream)
            {
                int[] positions = new int[2];

                Task<byte[]> getDataTask;
                var delegateStream = new DelegateStream(
                    (b,o,c) => 
                    {
                        
                        return 1; 
                    }, 
                    null);
            }

            private byte[] GetBuffer()
            {
                return null;
            }
        }

        private class QueueReader { }
    }
}
