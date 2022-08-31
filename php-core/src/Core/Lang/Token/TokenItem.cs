using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PHP.Core.Lang.Token
{
    public class TokenItem
    {
		public readonly TokenType type;
		public readonly int position;
		public readonly int line;
		public readonly int column;
		public readonly string data;

		public TokenItem(TokenType type, int position, int line, int column, string data)
        {
			this.type = type;
			this.position = position;
			this.line = line;
			this.column = column;
			this.data = data;
        }

        public override string ToString() => "[" + type.ToString() + "](" + data + "){p: " + position + " l: " + line + " c: " + column + "}";
    }
}
