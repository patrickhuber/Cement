using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class Int16Monoid : IMonoid<Int16>
    {
        public short Append(short first, short second)
        {
            return Convert.ToInt16(first + second);
        }

        public short Identity
        {
            get { return 0; }
        }
    }
}
