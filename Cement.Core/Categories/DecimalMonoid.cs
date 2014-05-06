using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class DecimalMonoid : IMonoid<decimal>
    {
        public decimal Append(decimal first, decimal second)
        {
            return first + second;
        }

        public decimal Identity
        {
            get { return decimal.Zero; }
        }
    }
}
