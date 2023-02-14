using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTFunctionNode : ASTListNode
    {
        public TokenItem Name = null;

        public ASTFunctionNode(TokenItem token) : base(token)
        {
        }

        public override string ToString(int offset)
        {
            return new string(' ', offset) + "function" + (Name == null ? "" : " " + Name.Data) + "()\n" + base.ToString(offset);
        }
        public override string ToString() => ToString(0);
    }
}
