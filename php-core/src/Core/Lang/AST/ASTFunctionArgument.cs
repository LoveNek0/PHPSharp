using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTFunctionArgument : ASTNode
    {
        public TokenItem Type => _type;

        internal TokenItem _type;
        internal ASTFunctionArgument(TokenItem token) : base(token)
        {
        }

        public override string ToString() => $"{(Type != null ? Type.Data + " " : "")}{Token.Data}";
    }
}
