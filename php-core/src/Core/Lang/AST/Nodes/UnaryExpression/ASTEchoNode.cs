using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.UnaryExpression
{
    public class ASTEchoNode : ASTUnaryNode
    {
        internal ASTEchoNode(TokenItem token, ASTNode operand = null) : base(token, OperatorSide.Left, operand)
        {
        }
    }
}
