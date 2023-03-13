using PHP.Runtime.Memory;
using PHP.Runtime.Memory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Lang
{
    internal class ASGExecutionBlock : ASGNode
    {
        List<ASGNode> list = new List<ASGNode>();
        public MemoryData Execute(MemoryBlock memory = null)
        {
            throw new NotImplementedException();
        }
    }
}
