using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Integration.IO;

namespace Integration.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<Person, PersonDto>> selector = p =>
                new PersonDto 
                { 
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    FullName = string.Format("{0}, {1}", p.LastName, p.FirstName)
                };
            XmlExpressionVisitor expressionVisitor = new XmlExpressionVisitor();
            var xml = expressionVisitor.ToXml(selector);
            Console.WriteLine(xml);

            PersonDto dto = new PersonDto();
            Person person = new Person { FirstName = "patrick" };
            Expression<Func<Person, string>> simpleTypeMap = m => m.FirstName;
            xml = expressionVisitor.ToXml(simpleTypeMap);
            Console.WriteLine(xml);
                        
            Expression<Func<Person, bool>> ruleTypeMap = m => m.FirstName == "patrick" && string.IsNullOrWhiteSpace(m.LastName);
            xml = expressionVisitor.ToXml(ruleTypeMap);
            Console.WriteLine(xml);

            Console.ReadLine();            
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
