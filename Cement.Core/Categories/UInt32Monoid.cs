using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class UInt32Monoid : IMonoid<UInt32>
    {
        public uint Append(uint first, uint second)
        {
            return first + second;
        }

        public uint Identity
        {
            get { return 0; }
        }
    }
}
