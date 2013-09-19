using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Windows;
using Integration.Transforms.Designer;
using System.Windows.Controls;
using Integration.Transforms;

namespace Integration.ConsoleApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var transform = CreateTransform();

            Grid grid = new Grid();
            grid.Children.Add(new TransformDesigner(transform));

            Window window = new Window();
            window.Content = grid;

            Application app = new Application();            
            app.Run(window);
        }

        private static Transform CreateTransform()
        {
            Transform transform = new Transform();
            transform.Inputs = new List<Model>(
                new Model[]
            {
                new Model{
                    Type = "clr-namespace:Integration.Test.Person",
                    Nodes = new List<Node> 
                    {                        
                        new Node {  Name = "FirstName", Id = Guid.NewGuid().ToString() },      
                        new Node {  Name = "LastName", Id = Guid.NewGuid().ToString() },
                        new Node
                        { 
                            Name = "AddressList", Id = Guid.NewGuid().ToString(),                            
                            Nodes = new List<Node>
                            {    
                                new Node
                                {                                    
                                    Name = "Address", Id = Guid.NewGuid().ToString(),
                                    Nodes = new List<Node>
                                    {
                                        new Node { Name = "Street", Id = Guid.NewGuid().ToString()},
                                        new Node { Name = "City", Id = Guid.NewGuid().ToString()},
                                        new Node { Name = "State", Id = Guid.NewGuid().ToString()},
                                        new Node { Name = "ZipCode", Id = Guid.NewGuid().ToString()},
                                        new Node { Name = "ZipCodePlus4", Id = Guid.NewGuid().ToString()}
                                    }
                                },
                            }
                        },
                        new Node{ Name = "DateOfBirth", Id = Guid.NewGuid().ToString() }
                    }
                }
            });
            transform.Outputs = new List<Model>( new Model[]
            {
                new Model{
                    Type = "clr-namespace:Integration.Test.PersonDto",
                    Nodes = new List<Node> 
                    {
                        new Node{ Name = "FirstName", Id = Guid.NewGuid().ToString() },
                        new Node{ Name = "LastName", Id = Guid.NewGuid().ToString() },
                        new Node{ Name = "FullName", Id = Guid.NewGuid().ToString() },
                    }
                }
            });
            return transform;
        }
    }
}
