using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class Int32Monoid : IMonoid<Int32>
    {
        public int Append(int first, int second)
        {
            return first + second;
        }

        public int Identity
        {
            get { return 0; }
        }        
    }
}
