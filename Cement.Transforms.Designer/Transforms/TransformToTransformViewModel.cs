using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cement.Transforms.Designer.ViewModels;

namespace Cement.Transforms.Designer.Transforms
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
        public IObjectFactory<TransformViewModel> TransformViewModelFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the transform model automatic transform model view model.
        /// </summary>
        /// <value>
        /// The transform model automatic transform model view model.
        /// </value>
        public ITransform<Model, TransformModelViewModel> TransformModelToTransformModelViewModel { get; protected set; }
        
        /// <summary>
        /// Gets or sets the transform function automatic function view model.
        /// </summary>
        /// <value>
        /// The transform function automatic function view model.
        /// </value>
        public ITransform<Function, FunctionViewModel> TransformFunctionToFunctionViewModel { get; protected set; }
        
        /// <summary>
        /// Gets or sets the transform operation automatic operation view model.
        /// </summary>
        /// <value>
        /// The transform operation automatic operation view model.
        /// </value>
        public ITransform<Operation, OperationViewModel> TransformOperationToOperationViewModel { get; protected set; }
        
        /// <summary>
        /// Gets or sets the transform link automatic link view model.
        /// </summary>
        /// <value>
        /// The transform link automatic link view model.
        /// </value>
        public ITransform<Link, LinkViewModel> TransformLinkToLinkViewModel { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformToTransformViewModel"/> class.
        /// </summary>
        /// <param name="transformViewModelFactory">The transform view model factory.</param>
        /// <param name="transformModelToTransformModelViewModel">The transform model automatic transform model view model.</param>
        public TransformToTransformViewModel(
            IObjectFactory<TransformViewModel> transformViewModelFactory,
            ITransform<Model, TransformModelViewModel> transformModelToTransformModelViewModel,
            ITransform<Function, FunctionViewModel> transformFunctionToFunctionViewModel,
            ITransform<Operation, OperationViewModel> transformOperationToOperationViewModel,
            ITransform<Link, LinkViewModel> transformLinkToLinkViewModel)
        {
            this.TransformViewModelFactory = transformViewModelFactory;
            this.TransformModelToTransformModelViewModel = transformModelToTransformModelViewModel;
            this.TransformFunctionToFunctionViewModel = transformFunctionToFunctionViewModel;
            this.TransformOperationToOperationViewModel = transformOperationToOperationViewModel;
            this.TransformLinkToLinkViewModel = transformLinkToLinkViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformToTransformViewModel"/> class.
        /// </summary>
        public TransformToTransformViewModel()
            : this(new GenericObjectFactory<TransformViewModel>(),
            null, null, null, null)
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
