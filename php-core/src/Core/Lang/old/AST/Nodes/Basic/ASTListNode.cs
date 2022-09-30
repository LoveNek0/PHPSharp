using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Basic
{
    public class ASTListNode : ASTNode
    {
        private List<ASTNode> list = new List<ASTNode>();
        public ASTListNode(TokenItem token) : base(token)
        {
        }

        public void Add(ASTNode node) => list.Add(node);
        public ASTNode Get(int index) => list[index];
        public int Count() => list.Count;

        public override string ToString()
        {
            return ToString(0);
        }
        public override string ToString(int offset)
        {
            string s = "";
            s += string.Join("", Enumerable.Repeat("\t", offset));
            s += token.Data + "\n";
            for (int i = 0; i < list.Count; i++)
            {
                s += string.Join("", Enumerable.Repeat("\t", offset));
                s += i + " => ";
                s += list[i].ToString(offset + 1) + "\n";
            }
            return s;
        }
    }
}
