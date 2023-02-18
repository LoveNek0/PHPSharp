using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTBinaryNode : ASTNode
    {
        internal ASTNode left;
        internal ASTNode right;
        internal uint deep;

        public ASTNode Left
        {
            get => left;
            private set => left = value;
        }
        public ASTNode Right
        {
            get => right;
            private set => right = value;
        }

        public ASTBinaryNode(TokenItem token, uint deep = 0, ASTNode left = null, ASTNode right = null) : base(token)
        {
            this.deep = deep;
            this.left = left;
            this.right = right;
        }

        public override string ToString(int offset) =>
            new string(' ', offset) + "(" + Left + " " + Token.Data + " " + Right + ")";
        public override string ToString() => ToString(0);
    }
}
