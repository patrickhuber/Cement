using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.Transforms
{
    public interface IObjectFactory<T>
    {
        T Add();
    }
}
