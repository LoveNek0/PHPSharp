using PHP.Core.Exceptions;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang
{
    public class Tokenizer
    {
        private string code;
        private int position;
        private int line;
        private int column;
        private bool isHTML;

        public Tokenizer(string code)
        {
            this.code = code;
            this.position = 0;
            this.line = 0;
            this.column = 0;
            this.isHTML = false;
        }

        void UpdatePosition()
        {
            line = 0;
            column = 0;
            for (int i = 0; i < code.Length && i < position; i++)
                if (code[i] == '\n') {
                    line++;
                    column = 0;
                }
                else
                    column++;
        }

        private TokenItem NextToken()
        {
            UpdatePosition();
            if (isHTML)
            {
                string HTML = "";
                for (int i = position; i < code.Length; i++)
                    if (TokenType.T_OPEN_TAG.GetPatternRegex().IsMatch(code.Substring(i)) || TokenType.T_OPEN_TAG_WITH_ECHO.GetPatternRegex().IsMatch(code.Substring(i)))
                        break;
                    else
                        HTML += code[i];
                TokenItem item = new TokenItem(TokenType.T_INLINE_HTML, position, this.line, this.column, HTML);
                position += HTML.Length;
                isHTML = false;
                if (HTML.Length > 0)
                    return item;
                return null;
            }
            string sub = this.code.Substring(this.position);
            foreach (TokenType type in TokenQueue.GetQueue()) {
                Match match = type.GetPatternRegex().Match(sub);
                if (match.Success && match.Index == 0)
                {
                    TokenItem token = new TokenItem(type, position, this.line, this.column, match.Value);
                    position += token.data.Length;
                    if (type == TokenType.T_CLOSE_TAG)
                        isHTML = true;
                    return token;
                }
            }
            throw new LexicalException("Unknown token \"" + code[position] + "\"", this.position, this.line, this.column);
        }

        public TokenItem[] GetTokens()
        {
            List<TokenItem> tokens = new List<TokenItem>();
            line = 0;
            column = 0;
            isHTML = true;
            while (position < code.Length)
            {
                TokenItem token = NextToken();
                if(token != null)
                    tokens.Add(token);
            }
            return tokens.ToArray();
        }

    }
}
