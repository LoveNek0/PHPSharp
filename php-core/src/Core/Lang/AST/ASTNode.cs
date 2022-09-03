using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTNode
    {
        public readonly TokenItem token;
        public ASTNode(TokenItem token) => this.token = token;

        public override string ToString()
        {
            return "(" + token.data + ")";
        }
    }
}
