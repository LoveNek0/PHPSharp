using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTWhileNode : ASTListNode
    {
        public ASTNode condition;
        public ASTWhileNode(TokenItem token) : base(token)
        {
        }

        public override string ToString(int offset)
        {
            return new string(' ', offset) + $"while({condition})\n" + base.ToString(offset);
        }
        public override string ToString() => ToString(0);
    }
}
