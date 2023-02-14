using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PHP.Core.Lang.Token
{
    public class TokenItem
    {
		public class TokenPosition
        {
			public readonly int Position;
			public readonly int Line;
			public readonly int Column;

			public TokenPosition(string code, int index)
            {
				this.Position = index;
				this.Line = 0;
				this.Column = 0;
				for(int i = 0; i < code.Length && i <= index; i++)
					if(code[i] == '\n')
                    {
						Line++;
						Column = 0;
                    }
                    else
						Column++;
            }
			public TokenPosition(int index, int line, int column)
            {
				Position = index;
				Line = line;
				Column = column;
            }

            public override string ToString() => "[" + Position + ":" + Line + "." + Column + "]";
        }

		public readonly TokenType Type;
		public readonly TokenPosition Position;
		public readonly string Data;

		public TokenItem(TokenType type, TokenPosition position, string data)
        {
			Type = type;
			Position = position;
			Data = data;
        }

        public override string ToString()
        {
			return ToString(0);
        }

        public string ToString(int offset)
        {
			string s = "";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "TokenItem(" + Data + "){\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\tInfo => {\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\t\tType: " + Type + ",\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			//s += "\t\tSide: " + Type.Info().Side + ",\n";
			//s += string.Join("", Enumerable.Repeat("\t", offset));
			//s += "\t\tOperator Type: " + Type.Info().Family + ",\n";
			//s += string.Join("", Enumerable.Repeat("\t", offset));
			//s += "\t\tPriority: " + Type.Info().Priority + ",\n";
			//s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\t\tPattern: " + Type.Info().Pattern + "\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\t},\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\tPosition{\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\t\tIndex: " + Position.Position + ",\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\t\tLine: " + Position.Line + ",\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\t\tColumn: " + Position.Column + "\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "\t}\n";
			s += string.Join("", Enumerable.Repeat("\t", offset));
			s += "}";
			return s;
		}
    }
}
