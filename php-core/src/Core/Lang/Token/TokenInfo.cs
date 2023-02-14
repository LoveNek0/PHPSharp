using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Token
{
    public class TokenInfo
    {
        public readonly TokenType Type;
        public readonly string Pattern;
        public readonly TokenType[] Expected;
        public Regex RegexPattern => new Regex(Pattern);

        public TokenInfo(TokenType type, string pattern, TokenType[] expected)
        {
            this.Type = type;
            this.Pattern = pattern;
            this.Expected = expected;
        } 

        public override string ToString() => "[" + Type + " => " + Pattern + "]";
    }
}
