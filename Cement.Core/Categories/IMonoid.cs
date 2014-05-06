using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public interface IMonoid<T>
    {
        T Append(T first, T second);
        T Identity { get; }
    }
}
