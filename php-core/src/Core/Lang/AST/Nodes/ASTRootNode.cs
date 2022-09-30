using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Structure
{
    public class ASTRootNode
    {
        private List<ASTNode> children = new List<ASTNode>();
        internal ASTRootNode()
        {
        }

        public ASTNode At(int index) => children[index];
        public int Count() => children.Count;
        public void Add(ASTNode node) => children.Add(node);

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Count(); i++)
                s += At(i) + "\n";
            return s;
        }
    }
}
