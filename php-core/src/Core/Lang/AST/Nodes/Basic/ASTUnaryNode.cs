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
        public enum OperatorSide
        {
            Left,
            Right
        }

        internal ASTNode operand;
        internal OperatorSide side;

        public ASTNode Operand
        {
            get { return operand; }
            private set { operand = value; }
        }
        public OperatorSide Side
        {
            get { return side; }
            private set { side = value; }
        }

        protected ASTUnaryNode(TokenItem token, OperatorSide side, ASTNode operand = null) : base(token)
        {
            this.side = side;
            this.operand = operand;
        }

        public override string ToString()
        {
            return "(" + (side == OperatorSide.Left ? Token.Data : "") +
                " " + operand + " " +
                (side == OperatorSide.Right ? Token.Data : "") + ")"; 
        }
    }
}
