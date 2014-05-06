using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class UInt16Monoid : IMonoid<UInt16>
    {
        public ushort Append(ushort first, ushort second)
        {
            return (ushort)(first + second);
        }

        public ushort Identity
        {
            get { return 0; }
        }
    }
}
