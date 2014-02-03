using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.Transforms
{
    /// <summary>
    /// defines a transformation between two types
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    public interface ITransform<TSource, TTarget>
    {
        /// <summary>
        /// Transforms the specified source, creating a new target.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>a new instance of the target.</returns>
        TTarget Transform(TSource source);

        /// <summary>
        /// Transforms the specified source onto the target.
        /// </summary>
        /// <param name="soruce">The soruce.</param>
        /// <param name="target">The target.</param>
        void Transform(TSource source, TTarget target);
    }
}
