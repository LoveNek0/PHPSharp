using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang;
using PHP.Core.Lang.AST;
using PHP.Core.Lang.Tokens;
using PHP.Runtime.Memory;
using PHP.Runtime.Memory.Data;

namespace PHP.Runtime
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            string code = File.ReadAllText(@"D:\Projects\CSharp\PHPSharp\php-core\test\e1.php");
            Lexer lexer = new Lexer(code);
            TokenItem[] tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            ASTRoot root = parser.Parse();
            Console.WriteLine(root);
            Console.WriteLine();
            Console.WriteLine("End.");
        }
    }
}
