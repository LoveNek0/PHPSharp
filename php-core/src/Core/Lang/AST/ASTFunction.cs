using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTFunction : ASTNode
    {
        public TokenItem Name => _name;
        public ASTFunctionArgument[] Arguments => _arguments.ToArray();
        public ASTBlock Block => _block;

        internal List<ASTFunctionArgument> _arguments = new List<ASTFunctionArgument>();
        internal TokenItem _name = null;
        internal ASTBlock _block = null;

        internal ASTFunction(TokenItem token) : base(token)
        {
        }

        public override string ToString() => $"[{(Name == null ? "Anonymouse" : "Named")}]{Token.Data}{(Name != null ? " " + Name.Data : "")}({String.Join(", ", _arguments)}){Block}";
    }
}
