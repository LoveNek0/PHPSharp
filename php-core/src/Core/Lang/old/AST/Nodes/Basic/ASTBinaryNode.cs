using PHP.Core.Lang.AST;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Basic
{
    public class ASTBinaryNode : ASTNode
    {
        public ASTNode leftOperand;
        public ASTNode rightOperand;
        public ASTBinaryNode(TokenItem token, ASTNode leftOperand = null, ASTNode rightOperand = null) : base(token)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public override string ToString()
        {
            return ToString(0);
        }
        public override string ToString(int offset)
        {
            string s = "";
            s += string.Join("", Enumerable.Repeat("\t", offset));
            s += "(" + leftOperand + " " + token.Data + " " + rightOperand + ")";
            return s;
        }
    }
}
