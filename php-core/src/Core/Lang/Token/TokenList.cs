using PHP.Core.Lang.Token;
using PHP.Core.Lang.Token.Info;
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
                TokenFamily.Loop,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[Ww][Hh][Ii][Ll][Ee]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_ECHO,
                TokenFamily.Loop,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[Ee][Cc][Hh][Oo]",
                new TokenType[]{

                }
            ),
            new TokenInfo(
                TokenType.T_CURLY_BRACE_OPEN,
                TokenFamily.Loop,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[{]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_CURLY_BRACE_CLOSE,
                TokenFamily.Loop,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[}]",
                new TokenType[]{
                    TokenType.T_CURLY_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_SEMICOLON,
                TokenFamily.EndOfLine,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[;]",
                new TokenType[]{
                    TokenType.T_SEMICOLON,
                    TokenType.T_VARIABLE,
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_WHITESPACE,
                TokenFamily.Ignore,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"\s+",
                new TokenType[]{}
            ),
            new TokenInfo(
                TokenType.T_ADD,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryLow,
                TokenSide.Left,
                @"[+]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_SUB,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryLow,
                TokenSide.Left,
                @"[-]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_MUL,
                TokenFamily.BinaryOparator,
                TokenPriority.Low,
                TokenSide.Left,
                @"[*]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_DIV,
                TokenFamily.BinaryOparator,
                TokenPriority.Low,
                TokenSide.Left,
                @"[/]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_POW,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryLow,
                TokenSide.Right,
                @"[*][*]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_MOD,
                TokenFamily.BinaryOparator,
                TokenPriority.Low,
                TokenSide.Left,
                @"[%]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Right,
                @"[=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_ADD_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Right,
                @"[+][=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_CONCAT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Left,
                @"[.]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_CONCAT_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Left,
                @"[.][=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_SUB_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Right,
                @"[-][=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_MUL_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Right,
                @"[*][=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_DIV_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Right,
                @"[/][=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_MOD_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Right,
                @"[%][=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_POW_ASSIGNMENT,
                TokenFamily.BinaryOparator,
                TokenPriority.VeryHigh,
                TokenSide.Right,
                @"[*][*][=]",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_LNUMBER,
                TokenFamily.Data,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[0-9]+",
                new TokenType[]{
                }
            ),
            new TokenInfo(
                TokenType.T_DNUMBER,
                TokenFamily.Data,
                TokenPriority.VeryLow,
                TokenSide.None,
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
                TokenFamily.Data,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"([$]+([a-zA-Z_][a-zA-Z0-9_]*))",
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
                TokenType.T_BRACE_OPEN,
                TokenFamily.Brace,
                TokenPriority.VeryHigh,
                TokenSide.None,
                @"[(]",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                    TokenType.T_BRACE_OPEN
                }
            ),
            new TokenInfo(
                TokenType.T_BRACE_CLOSE,
                TokenFamily.Brace,
                TokenPriority.VeryHigh,
                TokenSide.None,
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
                TokenFamily.TernaryOperator,
                TokenPriority.Normal,
                TokenSide.Right,
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
                TokenFamily.TernaryOperator,
                TokenPriority.Normal,
                TokenSide.Right,
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
                TokenFamily.UnaryOperator,
                TokenPriority.High,
                TokenSide.Right,
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
                TokenFamily.Ignore,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[<](([?]([pP][hH][pP])?)|[%])",
                new TokenType[]{
                    TokenType.T_LNUMBER,
                    TokenType.T_DNUMBER,
                    TokenType.T_VARIABLE,
                }
            ),
            new TokenInfo(
                TokenType.T_CLOSE_TAG,
                TokenFamily.Ignore,
                TokenPriority.VeryLow,
                TokenSide.None,
                @"[?%][>]",
                new TokenType[]{
                    TokenType.T_OPEN_TAG,
                    TokenType.T_OPEN_TAG_WITH_ECHO,
                    TokenType.T_INLINE_HTML
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
