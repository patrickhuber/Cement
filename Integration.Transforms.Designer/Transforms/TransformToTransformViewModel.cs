using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Integration.Transforms.Designer.ViewModels;

namespace Integration.Transforms.Designer.Transforms
{
    /// <summary>
    /// Defines a transformation between the view model and the model.
    /// </summary>
    public class TransformToTransformViewModel : ITransform<Transform, TransformViewModel>
    {
        /// <summary>
        /// Gets or sets the transform view model factory.
        /// </summary>
        /// <value>
        /// The transform view model factory.
        /// </value>
        IObjectFactory<TransformViewModel> TransformViewModelFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the transform model automatic transform model view model.
        /// </summary>
        /// <value>
        /// The transform model automatic transform model view model.
        /// </value>
        ITransform<Model, TransformModelViewModel> TransformModelToTransformModelViewModel { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformToTransformViewModel"/> class.
        /// </summary>
        /// <param name="transformViewModelFactory">The transform view model factory.</param>
        /// <param name="transformModelToTransformModelViewModel">The transform model automatic transform model view model.</param>
        public TransformToTransformViewModel(IObjectFactory<TransformViewModel> transformViewModelFactory,
            ITransform<Model, TransformModelViewModel> transformModelToTransformModelViewModel)
        {
            this.TransformViewModelFactory = transformViewModelFactory;
            this.TransformModelToTransformModelViewModel = transformModelToTransformModelViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformToTransformViewModel"/> class.
        /// </summary>
        public TransformToTransformViewModel()
            : this(new GenericObjectFactory<TransformViewModel>(),
            null)
        {
            throw new NotImplementedException("method is not implemented.");
        }

        /// <summary>
        /// Transforms the specified source creating a new target.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransformViewModel Transform(Transform source)
        {
            var target = new TransformViewModel();
            Transform(source, target);
            return target;
        }

        /// <summary>
        /// Transforms the specified source onto the target object
        /// </summary>
        /// <param name="soruce"></param>
        /// <param name="target"></param>
        public void Transform(Transform source, TransformViewModel target)
        {
            throw new NotImplementedException();
        }
    }
}
