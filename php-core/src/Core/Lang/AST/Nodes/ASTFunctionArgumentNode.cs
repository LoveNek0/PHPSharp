using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Nodes
{
    public class ASTFunctionArgumentNode
    {
        public TokenItem Type;
        public TokenItem Multiargument;
        public TokenItem Pointer;
        public TokenItem Name;
        public TokenItem DefaultValue;

        public ASTFunctionArgumentNode(TokenItem type, TokenItem multiargument, TokenItem pointer, TokenItem name, TokenItem defaultValue)
        {
            Type = type;
            Multiargument = multiargument;
            Pointer = pointer;
            Name = name;
            DefaultValue = defaultValue;
        }

        public override string ToString()
        {
            return (Type == null ? "" : Type.Data + " ") + (Multiargument == null ? "" : Multiargument.Data) + (Pointer == null ? "" : Pointer.Data + " ") + (Name == null ? "" : Name.Data + " ") + (DefaultValue == null ? "" : " = " + Type.Data);
        }
    }
}
