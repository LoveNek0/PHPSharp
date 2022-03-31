using PHP.Core.Lang.Lexic;
using PHP.Core.Lang.Lexic.Token;
using PHP.Core.Lang.Syntax;
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
            string code = @"<?php
namespace o;
function F1($a = 100){}

namespace a\b;
function F2($c, $e = 100, $f = null, $g){}

namespace;
$a = 800 - 200 * 350 + 5;
echo $a;
$b = 5 + (100 - 1 * $a) % 10 - 1001;
echo(($a+$b)-10);
";
            Lexer lexer = new Lexer(code);
            TokenItem[] list = lexer.Parse();
            foreach (TokenItem item in list)
                Console.WriteLine(item.ToString());
            Console.WriteLine();

            Syntaxer syntaxer = new Syntaxer(list);
            Console.WriteLine(syntaxer.Parse());
            Console.ReadKey();
        }
    }
}
