using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class ListMonoid<T> : IMonoid<List<T>>
    {
        public List<T> Append(List<T> first, List<T> second)
        {
            var list = new List<T>();
            list.AddRange(first);
            list.AddRange(second);
            return list;
        }

        public List<T> Identity
        {
            get { return new List<T> { }; }
        }
    }
}
