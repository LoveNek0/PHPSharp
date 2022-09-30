using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Data
{
    public class ASTDNumberNode : ASTDataNode<decimal>
    {
        public ASTDNumberNode(TokenItem token) : base(token, decimal.Parse(token.Data)) { }
    }
}
