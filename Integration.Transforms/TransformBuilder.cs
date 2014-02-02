using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Transforms
{
    /// <summary>
    /// Defines a transform builder that can be used to create transforms with a fluent interface.
    /// </summary>
    public class TransformBuilder
    {
        /// <summary>
        /// Gets or sets the transform.
        /// </summary>
        /// <value>
        /// The transform.
        /// </value>
        public Transform Transform { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformBuilder"/> class.
        /// </summary>
        public TransformBuilder() 
        {
            Transform = new Transform();
        }

        /// <summary>
        /// Inputses the specified inputs.
        /// </summary>
        /// <param name="inputs">The inputs.</param>
        /// <returns></returns>
        public TransformBuilder Inputs(Action<IObjectFactory<ModelBuilder>> inputs)
        {
            GenericObjectFactory<ModelBuilder> modelBuilderFactory = new GenericObjectFactory<ModelBuilder>();
            inputs(modelBuilderFactory);
            this.Transform.Inputs = new List<Model>(modelBuilderFactory.Items.Select(x => x.Model));
            return this;
        }

        /// <summary>
        /// Outputses the specified outputs.
        /// </summary>
        /// <param name="outputs">The outputs.</param>
        /// <returns></returns>
        public TransformBuilder Outputs(Action<IObjectFactory<ModelBuilder>> outputs)
        {
            GenericObjectFactory<ModelBuilder> modelBuilderFactory = new GenericObjectFactory<ModelBuilder>();
            outputs(modelBuilderFactory);
            this.Transform.Outputs = new List<Model>(modelBuilderFactory.Items.Select(x => x.Model));
            return this;
        }
    }
}
