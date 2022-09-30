using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Loop
{
    public class ASTWhileNode : ASTListNode
    {
        internal ASTNode condition;

        public ASTNode Condition
        {
            get { return condition; }
            private set { condition = value; }
        }

        internal ASTWhileNode(TokenItem token, ASTNode condition = null) : base(token)
        {
            this.condition = condition;
        }

        public override string ToString()
        {
            string s = "";
            s += "[" + Token.Data + "]";
            s += condition + "{\n";
            for (int i = 0; i < Count(); i++)
                s += At(i) + "\n";
            s += "}";
            return s;
        }
    }
}
