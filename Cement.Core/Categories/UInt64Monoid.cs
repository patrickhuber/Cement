using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class UInt64Monoid : IMonoid<UInt64>
    {
        public ulong Append(ulong first, ulong second)
        {
            return first + second;
        }

        public ulong Identity
        {
            get { return 0; }
        }
    }
}
