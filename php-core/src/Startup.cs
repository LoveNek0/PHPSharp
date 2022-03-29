using PHP.Core.Lang.Lexic;
using PHP.Core.Lang.Lexic.Token;
using System;

namespace PHP.Runtime {
    class Startup {
        public static void Main (string[] args) {
            string code = @"

wefwe
<?php
class PIDORAS{
    $ass = jopa;
    public function pizda(int $a)
            {

                return $zalupa;
            }

        }
$c = new class
?>
";
            Lexer lexer = new Lexer(code);
            TokenItem[] list = lexer.Parse();
            foreach(TokenItem item in list)
                Console.WriteLine(item.ToString());
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}