using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PHP.Core.Exceptions;
using PHP.Core.Lang.AST;
using PHP.Core.Lang.AST.Nodes;
using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using PHP.Core.Lang.Token.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang
{
    public class ASTBuilder
    {
        private TokenItem[] tokens;
        private int position;

        public ASTBuilder(TokenItem[] tokens)
        {
            TokenType[] toRemove =
            {
                TokenType.T_OPEN_TAG,
                TokenType.T_CLOSE_TAG,
                TokenType.T_INLINE_HTML,
                TokenType.T_WHITESPACE
            };
            List<TokenItem> list = new List<TokenItem>();
            foreach(TokenItem item in tokens)
                if(!toRemove.Contains(item.Type))
                    list.Add(item);
            this.tokens = list.ToArray();
            this.position = 0;
        }

        public TokenItem NextToken(params TokenType[] expected)
        {
            if(position >= tokens.Length)
                return null;
            TokenItem item = tokens[position];
            if(item.Type.Info().Family == TokenFamily.Ignore)
            {
                position++;
                return NextToken(expected);
            }
            if (expected.Contains(item.Type))
            {
                position++;
                return item;
            }
            throw new SyntaxException("Unexpected token \"" + item.Type + "\"", item.Position.Index, item.Position.Line, item.Position.Column);
        }
        public TokenItem NextToken(TokenInfo info) => NextToken(info.Expected);
        public TokenItem NextToken(TokenItem token) => NextToken(token.Type.Info());

        private TokenItem GetToken(int index)
        {
            if (index >= tokens.Length)
                return null;
            return tokens[index];
        }

        private ASTNode NextNode(TokenItem token, ASTNode prev = null)
        {
            if (token.Type == TokenType.T_SEMICOLON)
                return prev;

            return null;
        }

    }
}
