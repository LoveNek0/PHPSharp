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

        protected ASTBinaryNode(TokenItem token, uint deep, ASTNode left = null, ASTNode right = null) : base(token)
        {
            this.deep = deep;
            this.left = left;
            this.right = right;
        }

        public override string ToString() =>
            "(" + Left + " " + Token.Data + " " + Right + ")";
    }
}
