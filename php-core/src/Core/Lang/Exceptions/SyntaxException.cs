using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Exceptions
{
    public class SyntaxException : Exception
    {
        public readonly TokenPosition Position;
        public SyntaxException(string message, TokenPosition position) :
            base($"Syntax exception: {message} at line {position.Line + 1}, column {position.Column + 1}") => Position = position;
    }
}
