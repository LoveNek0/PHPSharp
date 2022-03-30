using PHP.Core.Lang.Lexic.Token;
using PHP.Core.Exceptions;
using PHP.Core.Lang.Syntax.AST;
using PHP.Core.Lang.Syntax.AST.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Syntax
{
    public class Syntaxer
    {
        private TokenItem[] tokens;
        private int current = 0;

        public Syntaxer(TokenItem[] tokens) => this.tokens = tokens;

        private TokenItem NextItem(params TokenType[] expected)
        {
            if (current >= tokens.Length)
                return null;
            if(expected == null || expected.Length == 0)
                return tokens[current++];
            if(expected.Contains(tokens[current].type))
                return tokens[current++];
            string message = "Unexpected " + tokens[current].type.ToString() + " \"" + tokens[current].data + "\", expected " + expected[0];
            if (expected.Length > 1)
            {
                for (int i = 1; i < expected.Length - 1; i++)
                    message += ", " + expected[i];
                message += " or " + expected[expected.Length - 1];
            }
            throw new SyntaxException(message, tokens[current].position, tokens[current].line, tokens[current].column);
        }

        private ASTNode ParseNext()
        {
            return new ASTListNode();
        }

        public ASTListNode Parse()
        {
            ASTListNode result = new ASTListNode();
            while (current < tokens.Length)
            {
                TokenItem next = NextItem();
                switch (next.type)
                {
                    case TokenType.T_LNUMBER:

                        break;
                }
            }
            return result;
        }
    }
}
