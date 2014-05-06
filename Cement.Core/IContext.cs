using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement
{
    public interface IContext
    {
        IDictionary<string, string> Attributes { get; }
    }
}
