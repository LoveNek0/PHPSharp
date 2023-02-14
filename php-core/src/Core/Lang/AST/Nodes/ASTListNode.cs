using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTListNode : ASTNode
    {
        private List<ASTNode> children = new List<ASTNode>();
        public ASTListNode(TokenItem token) : base(token)
        {
        }

        public ASTNode At(int index) => children[index];
        public int Count() => children.Count;
        public void Add(ASTNode node) => children.Add(node);

        public override string ToString(int offset)
        {
            string s = new string(' ', offset) + "{\n";
            for (int i = 0; i < children.Count; i++)
                s += children[i].ToString(offset + 4) + "\n";
            return s + new string(' ', offset) + "}";
        }
    }
}
