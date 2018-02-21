using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metasense.MetasenseFunctions
{
    public abstract class BaseFunction<T>
    {
        public abstract T Calculate();

        public virtual object Render()
        {
            return Calculate();
        }
    }
}
