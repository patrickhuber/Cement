using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Categories
{
    public class StringMonoid : IMonoid<string>
    {
        public string Append(string first, string second)
        {
            return first + second;
        }

        public string Identity
        {
            get { return string.Empty; }
        }
    }
}
