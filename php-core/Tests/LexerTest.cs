using PHP.Core.Lang.Lexic;
using PHP.Core.Lang.Lexic.Token;
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
            string code = @"
Hello, <?php
namespace \LexerTest\1
class Say{
    private static $word = 'world';
    public function GetWord() : string{
        return self::$word;
    }
}
$word = new Say();
echo $word->GetWord();

function press_f($var){
    for($i = 0; $i < $var; $i++)
        yield $i;
}
foreach(press_f(10) as $value)
    var_dump($value);
";
            Lexer lexer = new Lexer(code);
            TokenItem[] list = lexer.Parse();
            foreach (TokenItem item in list)
                Console.WriteLine(item.ToString());
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
