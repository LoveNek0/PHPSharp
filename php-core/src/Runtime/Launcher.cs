using PHP.Core.Lang;
using PHP.Core.Lang.AST;
using PHP.Core.Lang.Token;
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
            string code = System.IO.File.ReadAllText(@"test/e1.php");
            Tokenizer tokenizer = new Tokenizer(code);
            TokenItem[] items = tokenizer.GetTokens();
            foreach (var item in items)
                Console.WriteLine(item);
            ASTBuilder builder = new ASTBuilder(items);
            try
            {
                Console.WriteLine(builder.Build());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}

