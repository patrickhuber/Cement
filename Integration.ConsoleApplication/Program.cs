using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using Integration.Transforms;
using System.Xml.Serialization;
using System.IO;
using AutoMapper;
using Integration.Transforms.AutoMapper.Design;
using Integration.Transforms.Design.ViewModels;
using Integration.Transforms.Design;

namespace Integration.ConsoleApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Transform transform = new Transform();
            transform.Load("Transform.xml");

            IAutoMapperContext context = new AutoMapperContext();
            context.Configure();

            ITransform<Transform, TransformViewModel> mapper = new AutoMapperMapTransformToTransformViewModel(context);
            TransformViewModel transformViewModel = mapper.Transform(transform);
            
            Grid grid = new Grid();
            grid.Children.Add(new TransformDesigner(transform));

            Window window = new Window();
            window.Content = grid;

            Application app = new Application();            
            app.Run(window);
        }
    }
}
