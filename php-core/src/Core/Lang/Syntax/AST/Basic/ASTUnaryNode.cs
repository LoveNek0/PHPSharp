using PHP.Core.Lang.Lexic.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Syntax.AST.Basic
{
    public class ASTUnaryNode : ASTContainerNode
    {
        public ASTNode operand;
        public ASTUnaryNode(TokenItem token, ASTNode operand = null) : base(token) => this.operand = operand;

        public override string ToString() =>
            " (" + operand.ToString() + token.data + ") ";
    }
}
