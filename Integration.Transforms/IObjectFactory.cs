using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Transforms
{
    public interface IObjectFactory<T>
    {
        T Add();
    }
}
