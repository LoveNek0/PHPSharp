using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes.Basic
{
    public class ASTDataNode<T> : ASTNode
    {
        public readonly T Data;

        protected ASTDataNode(TokenItem token, T data) : base(token) => Data = data;

        public override string ToString() => "(" + Data.ToString() + ")";
    }
}
