using PHP.Core.Lang.Token.Info;
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
        public readonly TokenFamily Family;
        public readonly TokenPriority Priority;
        public readonly TokenSide Side;
        public readonly string Pattern;
        public readonly TokenType[] Expected;
        public Regex RegexPattern => new Regex(Pattern);

        public TokenInfo(TokenType type, TokenFamily family, TokenPriority priority, TokenSide side, string pattern, TokenType[] expected)
        {
            this.Type = type;
            this.Family = family;
            this.Priority = priority;
            this.Side = side;
            this.Pattern = pattern;
            this.Expected = expected;
        } 

        public override string ToString() => "[" + Type + "." + Priority + "@" + Pattern + "]";
    }
}
