using Integration.Transforms.Design.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Transforms.AutoMapper.Design
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperMapTransformToTransformViewModel
        : ITransform<Transform, TransformViewModel>
    {
        private IAutoMapperContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperMapTransformToTransformViewModel"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AutoMapperMapTransformToTransformViewModel(IAutoMapperContext context)
        {
            this.context = context;    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperMapTransformToTransformViewModel"/> class.
        /// </summary>
        public AutoMapperMapTransformToTransformViewModel()
            : this(new AutoMapperContext())
        { }

        /// <summary>
        /// Transforms the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransformViewModel Transform(Transform source)
        {
            return this.context.Engine.Map<Transform, TransformViewModel>(source);
        }

        /// <summary>
        /// Transforms the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public void Transform(Transform source, TransformViewModel target)
        {
            this.context.Engine.Map<Transform, TransformViewModel>(source, target);
        }
    }
}
