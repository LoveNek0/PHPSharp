using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTLambda : ASTNode
    {
        public ASTFunctionArgument[] Arguments => _arguments.ToArray();
        public TokenItem[] Use => _use.ToArray();
        public ASTBlock Block => _block;

        internal List<ASTFunctionArgument> _arguments = new List<ASTFunctionArgument>();
        internal List<TokenItem> _use = new List<TokenItem>();
        internal ASTBlock _block;

        internal ASTLambda(TokenItem token) : base(token)
        {
        }

        public override string ToString() => $"[Anonymouse]{Token.Data}{(_use.Count() > 0 ? $"use {String.Join(", ", _use)}" : "")}({String.Join(", ", _arguments)}){Block}";
    }
}
