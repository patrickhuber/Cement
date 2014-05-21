using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class Int64Monoid : IMonoid<Int64>
    {
        public long Append(long first, long second)
        {
            return first + second;
        }

        public long Identity
        {
            get { return 0L; }
        }
    }
}
