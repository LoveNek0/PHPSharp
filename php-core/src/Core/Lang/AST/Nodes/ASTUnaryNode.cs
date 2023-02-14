using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTUnaryNode : ASTNode
    {
        public enum UnaryOperatorType
        {
            None,
            Prefix,
            Postfix
        }

        internal ASTNode operand;
        internal UnaryOperatorType type;

        public ASTNode Operand
        {
            get { return operand; }
            private set { operand = value; }
        }
        public UnaryOperatorType Side
        {
            get { return type; }
            private set { type = value; }
        }

        public ASTUnaryNode(TokenItem token, UnaryOperatorType type = UnaryOperatorType.None, ASTNode operand = null) : base(token)
        {
            this.type = type;
            this.operand = operand;
        }

        public override string ToString(int offset)
        {
            return new string(' ', offset) + "(" + (type == UnaryOperatorType.Prefix || type == UnaryOperatorType.None ? Token.Data : "") +
                " " + operand + " " +
                (type == UnaryOperatorType.Postfix ? Token.Data : "") + ")"; 
        }
        public override string ToString() => ToString(0);
    }
}
