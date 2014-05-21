using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Adapters
{
    public class AdapterContext : IAdapterContext
    {
        public AdapterContext()
        {
            this.Attributes = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Attributes { get; private set; }
    }
}
