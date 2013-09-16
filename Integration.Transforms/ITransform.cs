using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Transforms
{
    public interface ITransform
    {
        object Transform(object source);
        void Transform(object source, object destination);
    }

    public interface ITransform<TSource, TDestination>
    {
        TDestination Transform(TSource source);
        void Transform(TSource source, TDestination destination);
    }
}
