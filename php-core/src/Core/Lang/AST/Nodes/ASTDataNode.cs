using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTDataNode : ASTNode
    {
        public ASTDataNode(TokenItem token) : base(token) { }

        public override string ToString(int offset) => new string(' ', offset) + "(" + this.Token.Data + ")";
        public override string ToString() => ToString(0);
    }
}
