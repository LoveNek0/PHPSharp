using PHP.Core.Lang;
using PHP.Core.Lang.AST;
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
         /*       public static void Main(string[] args)
        {
            string code = @"<?php
$a = 1 - 2 + 3;
";
            Tokenizer lexer = new Tokenizer(code);
            TokenItem[] list = lexer.GetTokens();
            foreach (TokenItem item in list)
                Console.WriteLine(item.ToString());
            Console.WriteLine();
            ASTBuilder builder = new ASTBuilder(list);
            ASTFile file = builder.Build();
            Console.WriteLine(file.ToString());
            Console.ReadKey();
        }*/
    }
}
