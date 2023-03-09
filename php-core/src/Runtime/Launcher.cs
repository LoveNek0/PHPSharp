using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace PHP.Runtime
{
    public class Launcher
    {
        public class ErrorListener : BaseErrorListener
        {
            public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                Console.WriteLine("{0}: line {1}/column {2} {3}", e, line, charPositionInLine, msg);
            }
        }
        public static void Main(string[] args)
        {
            string code = System.IO.File.ReadAllText(@"D:\Projects\CSharp\PHPSharp\php-core\test\e1.php");
            using (StreamReader fileStream = new StreamReader(@"D:\Projects\CSharp\PHPSharp\php-core\test\e1.php")) {
                AntlrInputStream inputStream = new AntlrInputStream(fileStream);

                SearchLexer lexer = new SearchLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
                SearchParser parser = new SearchParser(commonTokenStream);

                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ErrorListener()); // add ours

                parser.root();
            }

            /*Tokenizer tokenizer = new Tokenizer(code);
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
            */
            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}

