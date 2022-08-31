using PHP.Core.Lang;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Tests
{
    public class LexerTest
    {
                public static void Main(string[] args)
        {
            string code = @"Hello<?php
$a = 800 - 200 * 350 + 5;
echo $a;
$b = 5 + (100 - 1 * $a) % 10 - 1001;
echo(($a+$b)-10);?>
1
2
3
4
5
<?php ?>
";
            Tokenizer lexer = new Tokenizer(code);
            TokenItem[] list = lexer.GetTokens();
            foreach (TokenItem item in list)
                Console.WriteLine(item.ToString());
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
