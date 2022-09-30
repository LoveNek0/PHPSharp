using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Basic
{
    public class ASTUnaryNode : ASTNode
    {
        public ASTNode operand;
        public ASTUnaryNode(TokenItem token, ASTNode operand = null) : base(token) => this.operand = operand;

        public override string ToString()
        {
            return ToString(0);
        }
        public override string ToString(int offset)
        {
            string s = "";
            s += string.Join("", Enumerable.Repeat("\t", offset)); 
            s += "((" + token.Data + ") >> " + operand + ")";
            return s;
        }
    }
}
