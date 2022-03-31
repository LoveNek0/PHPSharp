using PHP.Core.Lang.Lexic.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Syntax.AST.Basic
{
    public class ASTContainerNode : ASTNode
    {
        public readonly TokenItem token;

        public ASTContainerNode(TokenItem token) => this.token = token;

        public override string ToString()
        {
            return token.data;
        }
    }
}
