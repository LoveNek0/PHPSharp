using PHP.Core.Lang.Lexic.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Syntax.AST.Basic
{
    public class ASTBinaryNode : ASTContainerNode
    {
        public ASTNode left;
        public ASTNode right;

        public ASTBinaryNode(TokenItem token, ASTNode left = null, ASTNode right = null) : base(token)
        {
            this.left = left;
            this.right = right;
        }

        public override string ToString() => 
            " (" + left.ToString() + " " + token.data + " " + right.ToString() + ") ";
    }
}
