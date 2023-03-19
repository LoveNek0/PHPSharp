using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang;
using PHP.Runtime.Memory;
using PHP.Runtime.Memory.Data;

namespace PHP.Runtime
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            Lexer lexer = new Lexer(File.ReadAllText(@"D:\Projects\CSharp\PHPSharp\php-core\test\e1.php"));
            foreach (var i in lexer.Tokenize())
                Console.WriteLine(i);
            Parser parser = new Parser(lexer.Tokenize());
            Console.WriteLine(parser.ParseFunction());
            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}
