using PHP.Runtime.Memory;
using PHP.Runtime.Memory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Lang
{
    internal interface ASGNode
    {
        public abstract MemoryData? Execute(MemoryBlock? memory = null);
    }
}
