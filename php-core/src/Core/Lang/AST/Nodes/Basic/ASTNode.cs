using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Basic
{
    public class ASTNode
    {
        public readonly TokenItem Token;
        protected ASTNode(TokenItem token) => Token = token;

        public override string ToString() => "(" + Token.Data + ")";
    }
}
