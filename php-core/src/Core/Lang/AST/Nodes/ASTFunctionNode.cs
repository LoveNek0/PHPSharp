using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTFunctionNode : ASTListNode
    {
        public TokenItem Name = null;
        public List<ASTFunctionArgumentNode> Arguments = new List<ASTFunctionArgumentNode>();
        public List<TokenItem> Use = new List<TokenItem>();

        public ASTFunctionNode(TokenItem token) : base(token)
        {
        }

        public override string ToString(int offset)
        {
            string s = new string(' ', offset) + "function" + (Name == null ? "" : " " + Name.Data) + "(";
            for (int i = 0; i < Arguments.Count; i++)
                s += Arguments[i] + (i >= Arguments.Count - 1 ? "" : ", ");
            s += ")";
            if(Use.Count > 0)
            {
                s += " use (";
                for (int i = 0; i < Use.Count; i++)
                    s += Use[i].Data + (i >= Use.Count - 1 ? "" : ", ");
                s += ")";
            }
            s+= base.ToString(offset);
            return s;
        }
        public override string ToString() => ToString(0);
    }
}
