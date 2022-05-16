using PHP.Core.Exceptions;
using PHP.Core.Lang.Lexic.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Lexic
{
    public class Lexer
    {
        private readonly string code;

        private int position = 0;

        private bool isCode = false;
        private int inlineHtmlStart = 0;
        
        public Lexer(string code) => this.code = code;

        private int GetLineAt(int position)
        {
            int line = 1;
            for(int i = 0; i < code.Length && i < position; i++)
                if(code[i] == '\n')
                    line++;
            return line;
        }
        private int GetColumnAt(int position)
        {
            int column = 1;
            for (int i = 0; i < code.Length && i < position; i++)
                if (code[i] == '\n')
                    column = 1;
                else
                    column++;
            return column;
        }

        private TokenItem ParseNext()
        {
            string currentCode = code.Substring(position);
            if (!isCode)
                if (TokenType.T_OPEN_TAG.GetPatternRegex().IsMatch(currentCode))
                {
                    isCode = true;
                    return new TokenItem(TokenType.T_INLINE_HTML,
                        code.Substring(inlineHtmlStart, position - inlineHtmlStart),
                        inlineHtmlStart, GetLineAt(inlineHtmlStart), GetColumnAt(inlineHtmlStart));
                }
                else
                {
                    if (position + 1 == code.Length)
                    {
                        TokenItem result = new TokenItem(TokenType.T_INLINE_HTML,
                        code.Substring(inlineHtmlStart, position - inlineHtmlStart),
                        inlineHtmlStart, GetLineAt(inlineHtmlStart), GetColumnAt(inlineHtmlStart));
                        position++;
                        return result;
                    }
                    position++;
                    return ParseNext();
                }
            else
            {
                TokenItem result = null;
                foreach (TokenType type in TokenQueue.GetQueue())
                {
                    Match match = type.GetPatternRegex().Match(currentCode);
                    if (match.Success)
                    {
                        if (type == TokenType.T_CLOSE_TAG)
                            isCode = false;
                        result = new TokenItem(type, match.Value, position, GetLineAt(position), GetColumnAt(position));
                        position += match.Value.Length;
                        inlineHtmlStart = position;
                        break;
                    }
                }
                if (result == null)
                    throw new LexicalException("Unable to recognize '" + (int)currentCode[0] + "'", position, GetLineAt(position), GetColumnAt(position));
                return result;
            }
        }

        private void Clean()
        {
            position = 0;
            isCode = false;
            inlineHtmlStart = 0;
        }

        public TokenItem[] Parse(bool clean = true)
        {
            Clean();
            List<TokenItem> list = new List<TokenItem>();
            while(position < code.Length)
                list.Add(ParseNext());
            if (clean)
            {
                list.RemoveAll(delegate (TokenItem item)
                {
                    return item.type == TokenType.T_COMMENT || item.type == TokenType.T_DOC_COMMENT || item.type == TokenType.T_WHITESPACE
                                    || item.type == TokenType.T_OPEN_TAG || item.type == TokenType.T_CLOSE_TAG;
                });
                list.RemoveAll(delegate (TokenItem item)
                {
                    return item.data.Length == 0;
                });
            }
            return list.ToArray();
        }
    }
}
