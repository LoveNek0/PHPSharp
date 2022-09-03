using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTUnaryNode : ASTNode
    {
        public ASTNode operand;
        public ASTUnaryNode(TokenItem token, ASTNode operand = null) : base(token) => this.operand = operand;

        public override string ToString()
        {
            return "((" + token.data + ") >> " + operand + ")";
        }
    }
}
