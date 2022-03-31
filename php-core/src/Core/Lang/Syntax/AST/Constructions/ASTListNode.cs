using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Syntax.AST.Constructions
{
    public class ASTListNode : ASTNode 
    {
        private List<ASTNode> children = new List<ASTNode>();

        public void AddNode(ASTNode node) => children.Add(node);

        public override string ToString()
        {
            string result = "[\n";
            foreach(ASTNode node in children)
                result += "    " + node.ToString() + ",\n";
            return result + "\n]";
        }
    }
}
