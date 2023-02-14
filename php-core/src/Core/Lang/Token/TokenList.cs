using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Token
{
    public static class TokenList
    {
        private static TokenInfo[] list =
        {
            new TokenInfo(
                TokenType.T_WHILE,
                @"[Ww][Hh][Ii][Ll][Ee]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_FOR,
                @"[Ff][Oo][Rr]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_FOREACH,
                @"[Ff][Oo][Rr][Ee][Aa][Cc][Hh]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_ECHO,
                @"[Ee][Cc][Hh][Oo]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_FUNCTION,
                "[Ff][Uu][Nn][Cc][Tt][Ii][Oo][Nn]",
                new TokenType[]
                {
                    TokenType.T_STRING,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_RETURN,
                @"[Rr][Ee][Tt][Uu][Rr][Nn]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_CURLY_BRACE_OPEN,
                @"[{]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_CURLY_BRACE_CLOSE,
                @"[}]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_SEMICOLON,
                @"[;]",
                new TokenType[]{
                    TokenType.T_SEMICOLON,
                    TokenType.T_VARIABLE,
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_WHITESPACE,
                @"\s+",
                new TokenType[]{}
            ),
            new TokenInfo(
                TokenType.T_ADD,
                @"[+]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_SUB,
                @"[-]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_MUL,
                @"[*]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_DIV,
                @"[/]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_POW,
                @"[*][*]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_MOD,
                @"[%]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_ASSIGNMENT,
                @"[=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_FUNCTION,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_ADD_ASSIGNMENT,
                @"[+][=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_CONCAT,
                @"[.]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_CONCAT_ASSIGNMENT,
                @"[.][=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_SUB_ASSIGNMENT,
                @"[-][=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_MUL_ASSIGNMENT,
                @"[*][=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_DIV_ASSIGNMENT,
                @"[/][=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_MOD_ASSIGNMENT,
                @"[%][=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_POW_ASSIGNMENT,
                @"[*][*][=]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_LNUMBER,
                @"[0-9]+",
                new TokenType[]{
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON,
                    TokenType.T_QUERY,
                    TokenType.T_COLON
                }
            ),
            new TokenInfo(
                TokenType.T_DNUMBER,
                @"([0-9]+)[.]([0-9]+)",
                new TokenType[]{
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON,
                    TokenType.T_QUERY,
                    TokenType.T_COLON
                }
            ),
            new TokenInfo(
                TokenType.T_VARIABLE,
                @"([$]+([a-zA-Z_][a-zA-Z0-9_]*))",
                new TokenType[]{
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_ASSIGNMENT,
                    TokenType.T_ADD_ASSIGNMENT,
                    TokenType.T_SUB_ASSIGNMENT,
                    TokenType.T_MUL_ASSIGNMENT,
                    TokenType.T_DIV_ASSIGNMENT,
                    TokenType.T_MOD_ASSIGNMENT,
                    TokenType.T_POW_ASSIGNMENT,
                    TokenType.T_CONCAT_ASSIGNMENT,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON,
                    TokenType.T_QUERY,
                    TokenType.T_COLON,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_BRACE_OPEN,
                @"[(]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN,
                    TokenType.T_INCREMENT,
                    TokenType.T_DECREMENT
                }
            ),
            new TokenInfo(
                TokenType.T_BRACE_CLOSE,
                @"[)]",
                new TokenType[]{
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_ASSIGNMENT,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON,
                    TokenType.T_QUERY,
                    TokenType.T_COLON
                }
            ),
            new TokenInfo(
                TokenType.T_QUERY,
                @"[?]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_COLON,
                @"[:]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_OPEN_TAG_WITH_ECHO,
                @"<[?%]=",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_OPEN_TAG,
                @"[<](([?]([pP][hH][pP])?)|[%])",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                }
            ),
            new TokenInfo(
                TokenType.T_CLOSE_TAG,
                @"[?%][>]",
                new TokenType[]{
                    TokenType.T_OPEN_TAG,
                    TokenType.T_OPEN_TAG_WITH_ECHO,
                    TokenType.T_INLINE_HTML
                }
            ),
            new TokenInfo(
                TokenType.T_STRING, 
                @"[a-zA-Z_][a-zA-Z0-9_]*", 
                new TokenType[] {
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON
                }
            ),
            new TokenInfo(
                TokenType.T_INCREMENT,
                @"[+][+]",
                new TokenType[] {
                    TokenType.T_VARIABLE,
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON
                }
            ),

            
            /*
                TokenInfoList.Add(new TokenInfo(TokenType.T_CONSTANT_ENCAPSED_STRING, TokenSide.Left, TokenFamily.Data, 0, "\\\"([\\S\\s]*?)\\\"|\\'([\\S\\s]*?)\\'", new TokenType[] {
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON
                }));
                TokenInfoList.Add(new TokenInfo(TokenType.T_STRING, TokenSide.Left, TokenFamily.Data, 0, @"[a-zA-Z_][a-zA-Z0-9_]*", new TokenType[] {
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_BRACE_CLOSE,
                    TokenType.T_SEMICOLON
                }));

                TokenInfoList.Add(new TokenInfo(TokenType.T_DOLLAR_OPEN_CURLY_BRACES, TokenSide.Left, TokenFamily.Data, 0, @"${", new TokenType[] {
                    TokenType.T_ADD,
                    TokenType.T_SUB,
                    TokenType.T_MUL,
                    TokenType.T_DIV,
                    TokenType.T_POW,
                    TokenType.T_MOD,
                    TokenType.T_ASSIGNMENT
                }));


                //  Unused Tokens
                TokenInfoList.Add(new TokenInfo(TokenType.T_DOC_COMMENT, TokenSide.Left, TokenFamily.Data, TokenPriority.VeryLow, @"\/\*\*[\s\S]*\*\/", new TokenType[] { }));
                TokenInfoList.Add(new TokenInfo(TokenType.T_COMMENT, TokenSide.Left, TokenFamily.Data, TokenPriority.VeryLow, @"(((\/\/)|#).*)|(\/\*[\s\S]*\*\/)", new TokenType[] { }));
                TokenInfoList.Add(new TokenInfo(TokenType.T_WHITESPACE, TokenSide.Left, TokenFamily.Data, TokenPriority.VeryLow, @"[\s]+", new TokenType[] { }));

            
                //  Separators
                TokenInfoList.Add(new TokenInfo(TokenType.T_COMMA, TokenSide.Left, TokenFamily.Data, 0, @"[,]", new TokenType[] { }));
                TokenInfoList.Add(new TokenInfo(TokenType.T_SEMICOLON, TokenSide.Left, TokenFamily.Data, 0, @";", new TokenType[] { }));

                //  Braces
                TokenInfoList.Add(new TokenInfo(TokenType.T_CURLY_BRACE_OPEN, TokenSide.Left, TokenFamily.Data, 0, @"\{", new TokenType[] { }));
                TokenInfoList.Add(new TokenInfo(TokenType.T_CURLY_BRACE_CLOSE, TokenSide.Left, TokenFamily.Data, 0, @"\}", new TokenType[] { }));
                TokenInfoList.Add(new TokenInfo(TokenType.T_SQUARE_BRACE_OPEN, TokenSide.Left, TokenFamily.Data, 0, @"\[", new TokenType[] { }));
                TokenInfoList.Add(new TokenInfo(TokenType.T_SQUARE_BRACE_CLOSE, TokenSide.Left, TokenFamily.Data, 0, @"\]", new TokenType[] { }));
            */
        };

        public static TokenInfo Info(this TokenType type)
        {
            foreach (TokenInfo info in list)
                if (info.Type == type)
                    return info;
            return null;
        }
    }
}
