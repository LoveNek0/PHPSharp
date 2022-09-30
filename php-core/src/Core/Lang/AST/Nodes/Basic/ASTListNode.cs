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
        private List<ASTNode> children = new List<ASTNode>();
        protected ASTListNode(TokenItem token) : base(token)
        {
        }

        public ASTNode At(int index) => children[index];
        public int Count() => children.Count;
        public void Add(ASTNode node) => children.Add(node);

    }
}
