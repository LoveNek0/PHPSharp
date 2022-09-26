using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Token.Info
{
    public enum TokenFamily
    {
        Ignore,
        EndOfLine,
        Separator,
        Brace,
        Data,
        UnaryOperator,
        BinaryOparator,
        TernaryOperator
    }
}
