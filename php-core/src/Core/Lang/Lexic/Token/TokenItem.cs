using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Lexic.Token
{
    public class TokenItem
    {
        public readonly TokenType type;
        public readonly string data;

        public readonly int position;
        public readonly int line;
        public readonly int column;

        public TokenItem(TokenType type, string data, int position, int line, int column)
        {
            this.type = type;
            this.data = data;
            this.position = position;
            this.line = line;
            this.column = column;
        }

        public override string ToString()
        {
            return "\"" + data + "\" [" + type + "] on line " + line + ", column " + column;
        }
    }
}
