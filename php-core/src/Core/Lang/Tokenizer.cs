using PHP.Core.Exceptions;
using PHP.Core.Lang.Token;
using PHP.Core.Lang.Token.Info;
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
        private static TokenType[] queue =
        {
            TokenType.T_WHITESPACE,
            TokenType.T_OPEN_TAG_WITH_ECHO,
            TokenType.T_OPEN_TAG,
            TokenType.T_CLOSE_TAG,
            TokenType.T_SEMICOLON,
            TokenType.T_ADD,
            TokenType.T_SUB,
            TokenType.T_POW,
            TokenType.T_MUL,
            TokenType.T_DIV,
            TokenType.T_MOD,
            TokenType.T_ASSIGNMENT,
            TokenType.T_LNUMBER,
            TokenType.T_DNUMBER,
            TokenType.T_VARIABLE,
            TokenType.T_BRACE_OPEN,
            TokenType.T_BRACE_CLOSE
        };
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
                    if (TokenType.T_OPEN_TAG.Info().RegexPattern.IsMatch(code.Substring(i)) 
                        ||
                        TokenType.T_OPEN_TAG_WITH_ECHO.Info().RegexPattern.IsMatch(code.Substring(i)))
                        break;
                    else
                        HTML += code[i];
                TokenItem item = new TokenItem(TokenType.T_INLINE_HTML, new TokenItem.TokenPosition(this.code, position), HTML);
                position += HTML.Length;
                isHTML = false;
                if (HTML.Length > 0)
                    return item;
                return null;
            }
            string sub = this.code.Substring(this.position);
            foreach (TokenType info in queue) {
                Match match = info.Info().RegexPattern.Match(sub);
                if (match.Success && match.Index == 0)
                {
                    TokenItem token = new TokenItem(info, new TokenItem.TokenPosition(this.code, position), match.Value);
                    position += token.Data.Length;
                    if (info == TokenType.T_CLOSE_TAG)
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
