using Integration.Transforms.Designer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Transforms.AutoMapper.Designer
{
    /// <summary>
    /// Creates the designer automapper profile.
    /// </summary>
    public class DesignerProfile : global::AutoMapper.Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Transform, TransformViewModel>();
            CreateMap<TransformViewModel, Transform>();

            CreateMap<Model, TransformModelViewModel>();
            CreateMap<TransformModelViewModel, Model>();

            CreateMap<Function, FunctionViewModel>();
            CreateMap<FunctionViewModel, Function>();

            CreateMap<Link, LinkViewModel>();
            CreateMap<LinkViewModel, Link>();            
                        
            CreateMap<Operation, OperationViewModel>();
            CreateMap<OperationViewModel, Operation>();

            CreateMap<Node, NodeViewModel>();
            CreateMap<NodeViewModel, Node>();

            CreateMap<Position, PositionViewModel>();
            CreateMap<PositionViewModel, Position>();

            CreateMap<Input, InputViewModel>();
            CreateMap<InputViewModel, Input>();

            CreateMap<Output, OutputViewModel>();
            CreateMap<OutputViewModel, Output>();
                        
            CreateMap<Parameter, ParameterViewModel>();
            CreateMap<ParameterViewModel, Parameter>();
        }

        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>
        /// The name of the profile.
        /// </value>
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }
    }
}
