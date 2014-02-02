using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Referee.Transforms
{
    public interface IObjectFactory<T>
    {
        T Add();
    }
}
