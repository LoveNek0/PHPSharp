using PHP.Core.Lang.Lexic.Token;
using PHP.Core.Lang.Syntax.AST;
using PHP.Core.Lang.Syntax.AST.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Syntax.AST.Constructions
{
    public class ASTFunctionNode : ASTNode
    {
        public TokenItem token;
        public ASTContainerNode ns;
        public ASTContainerNode name;
        public ASTListNode args;
        public ASTListNode body;

        public override string ToString()
        {
            return "{" + token.data + " " + ns.token.data + " " + name.token.data + " " + args + "}";
        }
    }
}
