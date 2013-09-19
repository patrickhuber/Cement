using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Transforms
{
    public class TransformBuilder
    {
        public Transform Transform { get; protected set; }

        public TransformBuilder() 
        {
            Transform = new Transform();
        }

        public TransformBuilder Inputs(Action<IObjectFactory<ModelBuilder>> inputs)
        {
            GenericObjectFactory<ModelBuilder> modelBuilderFactory = new GenericObjectFactory<ModelBuilder>();
            inputs(modelBuilderFactory);
            this.Transform.Inputs = new List<Model>(modelBuilderFactory.Items.Select(x => x.Model));
            return this;
        }

        public TransformBuilder Outputs(Action<IObjectFactory<ModelBuilder>> outputs)
        {
            GenericObjectFactory<ModelBuilder> modelBuilderFactory = new GenericObjectFactory<ModelBuilder>();
            outputs(modelBuilderFactory);
            this.Transform.Outputs = new List<Model>(modelBuilderFactory.Items.Select(x => x.Model));
            return this;
        }
    }
}
