﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PHP.Core.Lang.Token.TokenItem;

namespace PHP.Core.Exceptions
{
    public class SyntaxException : Exception
    {
        public readonly int position;
        public readonly int line;
        public readonly int column;

        public SyntaxException(string message, TokenPosition position) : this(message, position.Position, position.Line, position.Column) { }
        public SyntaxException(string message, int position, int line, int column) :
            base("Syntax error: " + message + " on line " + line + " column " + column)
        {
            this.position = position;
            this.line = line;
            this.column = column;
        }
    }
}
