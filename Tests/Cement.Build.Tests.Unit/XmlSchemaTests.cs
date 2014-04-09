using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace Cement.Build.Tests.Unit
{
    /// <summary>
    /// Summary description for XmlSchemaTests
    /// </summary>
    [TestClass]
    public class XmlSchemaTests
    {
        public XmlSchemaTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_XmlSchema_Read_Adds_Validation_Errors()
        {
            var memoryStream = new MemoryStream();
            var buffer = Encoding.ASCII.GetBytes(GetValidSchema());
            memoryStream.Write(buffer, 0, buffer.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var xmlTextReader = new XmlTextReader(memoryStream);
            xmlTextReader.XmlResolver = null;
            var xmlSchema = XmlSchema.Read(
                xmlTextReader, 
                new ValidationEventHandler((o, args) => 
                {
                    Assert.Fail();
                }));
        }
       
        private string GetValidSchema()
        {
            return @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
                <xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">

                <xs:simpleType name=""stringtype"">
                  <xs:restriction base=""xs:string""/>
                </xs:simpleType>

                <xs:simpleType name=""inttype"">
                  <xs:restriction base=""xs:positiveInteger""/>
                </xs:simpleType>

                <xs:simpleType name=""dectype"">
                  <xs:restriction base=""xs:decimal""/>
                </xs:simpleType>

                <xs:simpleType name=""orderidtype"">
                  <xs:restriction base=""xs:string"">
                    <xs:pattern value=""[0-9]{6}""/>
                  </xs:restriction>
                </xs:simpleType>

                <xs:complexType name=""shiptotype"">
                  <xs:sequence>
                    <xs:element name=""name"" type=""stringtype""/>
                    <xs:element name=""address"" type=""stringtype""/>
                    <xs:element name=""city"" type=""stringtype""/>
                    <xs:element name=""country"" type=""stringtype""/>
                  </xs:sequence>
                </xs:complexType>

                <xs:complexType name=""itemtype"">
                  <xs:sequence>
                    <xs:element name=""title"" type=""stringtype""/>
                    <xs:element name=""note"" type=""stringtype"" minOccurs=""0""/>
                    <xs:element name=""quantity"" type=""inttype""/>
                    <xs:element name=""price"" type=""dectype""/>
                  </xs:sequence>
                </xs:complexType>

                <xs:complexType name=""shipordertype"">
                  <xs:sequence>
                    <xs:element name=""orderperson"" type=""stringtype""/>
                    <xs:element name=""shipto"" type=""shiptotype""/>
                    <xs:element name=""item"" maxOccurs=""unbounded"" type=""itemtype""/>
                  </xs:sequence>
                  <xs:attribute name=""orderid"" type=""orderidtype"" use=""required""/>
                </xs:complexType>

                <xs:element name=""shiporder"" type=""shipordertype""/>

                </xs:schema>";
        }
    }
}
