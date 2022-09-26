using PHP.Core.Lang.AST.Nodes.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTFile
    {
        private List<ASTNode> nodes = new List<ASTNode>();

        public ASTFile()
        {

        }

        public void Add(ASTNode node) => nodes.Add(node);
        public ASTNode Get(int index) => nodes[index];
        public int Count => nodes.Count;

        public override string ToString()
        {
            string s = "File {\n";
            int i = 1;
            foreach(ASTNode node in nodes)
                s += i + " >> " + node.ToString() + "\n";
            return s + "}";
        }
    }
}
