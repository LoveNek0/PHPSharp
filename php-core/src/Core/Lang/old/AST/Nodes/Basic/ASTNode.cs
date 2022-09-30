using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Basic
{
    public class ASTNode
    {
        public readonly TokenItem token;
        public ASTNode parent = null;
        public ASTNode(TokenItem token) => this.token = token;
        public ASTNode(TokenItem token, ASTNode parent)
        {
            this.token = token;
            this.parent = parent;
        }

        public override string ToString()
        {
            return ToString(0);
        }
        public virtual string ToString(int offset)
        {
            string s = "";
            s += string.Join("", Enumerable.Repeat("\t", offset));
            s += "(" + token.Data + ")";
            return s;
        }
    }
}
