using PHP.Core.Lang.AST.Nodes.Basic;
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
        public ASTNode expression;
        public ASTWhileNode(TokenItem token) : base(token)
        {
        }
    }
}
