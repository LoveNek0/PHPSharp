using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Data
{
    public class ASTVariableNode : ASTDataNode<string>
    {
        public ASTVariableNode(TokenItem token) : base(token, token.Data) { }
    }
}
