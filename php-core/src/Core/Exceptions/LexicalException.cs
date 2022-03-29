using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Exceptions
{
    public class LexicalException : Exception
    {
        public readonly int position;
        public readonly int line;
        public readonly int column;

        public LexicalException(string message, int position, int line, int column) :
            base("Lexical error: " + message + " on line " + line + " column " + column)
        {
            this.position = position;
            this.line = line;
            this.column = column;
        }
    }
}
