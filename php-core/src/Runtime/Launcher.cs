using PHP.Core.Lang;
using PHP.Core.Lang.AST;
using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using PHP.Core.Lang.Token.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            string code = System.IO.File.ReadAllText(@"D:\Projects\CSharp\PHPSharp\php-core\test\e1.php");
            Tokenizer tokenizer = new Tokenizer(code);
            TokenItem[] items = tokenizer.GetTokens();
            foreach (var item in items)
                Console.WriteLine(item);
            ASTBuilder builder = new ASTBuilder(items);
            
            Console.WriteLine(builder.Build());
            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}

