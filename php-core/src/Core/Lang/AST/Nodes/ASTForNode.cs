using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTForNode : ASTListNode
    {
        public ASTNode Initializer;
        public ASTNode Condition;
        public ASTNode Action;
        public ASTForNode(TokenItem token) : base(token)
        {
        }

        public override string ToString(int offset)
        {
            return new string(' ', offset) + $"for({Initializer}; {Condition}; {Action})\n" + base.ToString(offset);
        }
        public override string ToString() => ToString(0);
    }
}
