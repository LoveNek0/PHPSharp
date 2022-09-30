using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.BinaryExpression
{
    public class ASTMulNode : ASTBinaryNode
    {
        public ASTMulNode(TokenItem token, uint deep, ASTNode left = null, ASTNode right = null) : base(token, deep, left, right) { }
    }
}
