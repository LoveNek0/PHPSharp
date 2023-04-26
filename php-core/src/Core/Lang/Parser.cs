using PHP.Core.Lang.AST;
using PHP.Core.Lang.Exceptions;
using PHP.Core.Lang.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang.AST.Operators;
using PHP.Core.Lang.AST.Structures;
using PHP.Core.Lang.AST.Structures.Function;

namespace PHP.Core.Lang
{
    public class Parser
    {
        private static readonly TokenType[] ignoreTokenTypes =
        {
            TokenType.T_OPEN_TAG,
            TokenType.T_CLOSE_TAG,
            TokenType.T_WHITESPACE,
            TokenType.T_COMMENT
        };

        private TokenItem[] tokens;
        private int index;

        public Parser(TokenItem[] tokens)
        {
            this.tokens = (from TokenItem item in tokens where !ignoreTokenTypes.Contains(item.Type) select item)
                .ToArray();
        }

        private TokenItem GetToken(int offset = 0)
        {
            if (index + offset < 0 || index + offset >= tokens.Length)
                throw new Exception("Trying to get token item out of bounds");
            return tokens[index + offset];
        }

        private TokenItem GetToken(int offset = 0, params TokenType[] expected)
        {
            TokenItem token = GetToken(offset);
            if (expected.Length == 0 || expected.Contains(token.Type))
                return token;
            throw new UnexpectedTokenException(token, expected);
        }

        private TokenItem GetToken(params TokenType[] expected) => GetToken(0, expected);

        private TokenItem NextToken(params TokenType[] expected)
        {
            TokenItem token = GetToken(expected);
            index++;
            return token;
        }

        private bool IsMatch(int offset, params TokenType[] expected)
        {
            TokenItem token = GetToken(offset);
            return expected.Length == 0 || expected.Contains(token.Type);
        }

        private bool IsMatch(params TokenType[] expected) => IsMatch(0, expected);

        public ASTRoot Parse()
        {
            ASTRoot root = new ASTRoot();
            while (!IsMatch(TokenType.T_EOF))
            {
                if (IsMatch(TokenType.T_SEMICOLON))
                {
                    NextToken();
                    continue;
                }
                root._children.Add(ParseLine());
            }
            return root;
        }

        private ASTNode ParseLine()
        {
            if (IsMatch(TokenType.T_SEMICOLON))
            {
                NextToken(TokenType.T_SEMICOLON);
                return ParseLine();
            }
            if (IsMatch(TokenType.T_IF))
                return ParseIf();
            if (IsMatch(TokenType.T_WHILE))
                return ParseWhile();
            if (IsMatch(TokenType.T_DO))
                return ParseDoWhile();
            if (IsMatch(TokenType.T_FUNCTION))
                return ParseFunction();
            if (IsMatch(TokenType.T_VARIABLE, TokenType.T_STATIC_STRING))
            {
                ASTNode node = ParseExpression();
                NextToken(TokenType.T_SEMICOLON);
                return node;
            }

            throw new UnexpectedTokenException(GetToken());
        }

        private ASTNode ParseIf()
        {
            ASTIf node = new ASTIf(NextToken(TokenType.T_IF));
            NextToken(TokenType.T_BRACE_OPEN);
            node._expression = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._ifBlock.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._ifBlock.Add(ParseLine());
            if (IsMatch(TokenType.T_ELSE))
            {
                NextToken(TokenType.T_ELSE);
                if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
                {
                    NextToken(TokenType.T_CURLY_BRACE_OPEN);
                    while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                        node._elseBlock.Add(ParseLine());
                    NextToken(TokenType.T_CURLY_BRACE_CLOSE);
                }
                else
                    node._elseBlock.Add(ParseLine());
            }
            return node;
        }
        private ASTNode ParseWhile()
        {
            ASTWhile node = new ASTWhile(NextToken(TokenType.T_WHILE));
            NextToken(TokenType.T_BRACE_OPEN);
            node._expression = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._lines.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._lines.Add(ParseLine());

            return node;
        }
        private ASTNode ParseDoWhile()
        {
            ASTDoWhile node = new ASTDoWhile(NextToken(TokenType.T_DO));
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._lines.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._lines.Add(ParseLine());
            NextToken(TokenType.T_WHILE);
            NextToken(TokenType.T_BRACE_OPEN);
            node._expression = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            return node;
        }
        private ASTNode ParseFunction()
        {
            ASTFunction node = new ASTFunction(NextToken(TokenType.T_FUNCTION));
            node._name = NextToken(TokenType.T_STATIC_STRING);
            NextToken(TokenType.T_BRACE_OPEN);
            while (true)
            {
                if (IsMatch(TokenType.T_BRACE_CLOSE))
                    break;
                ASTFunctionArgument argument = new ASTFunctionArgument();
                if (IsMatch(TokenType.T_STATIC_STRING))
                    argument._type = NextToken(TokenType.T_STATIC_STRING);
                if (IsMatch(TokenType.T_BIT_AND))
                {
                    NextToken(TokenType.T_BIT_AND);
                    argument._isPointer = true;
                }
                argument._variable = NextToken(TokenType.T_VARIABLE);
                if (IsMatch(TokenType.T_ASSIGNMENT))
                {
                    NextToken(TokenType.T_ASSIGNMENT);
                    argument._default = ParseAddition();
                }
                node._arguments.Add(argument);
                if (IsMatch(TokenType.T_COMMA))
                {
                    NextToken(TokenType.T_COMMA);
                    continue;
                }
                break;
            }
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.T_COLON))
            {
                NextToken(TokenType.T_COLON);
                node._returnType = NextToken(TokenType.T_STATIC_STRING);
            }
            NextToken(TokenType.T_CURLY_BRACE_OPEN);
            while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                node._lines.Add(ParseLine());
            NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            return node;
        }

        private ASTNode ParseExpression()
        {
            return ParseAssignment();
        }
        private ASTNode ParseAssignment()
        {
            ASTNode left = ParseAddition();
            if (IsMatch(
                    TokenType.T_ASSIGNMENT,
                    TokenType.T_ADD_ASSIGNMENT,
                    TokenType.T_SUB_ASSIGNMENT,
                    TokenType.T_MUL_ASSIGNMENT,
                    TokenType.T_DIV_ASSIGNMENT,
                    TokenType.T_POW_ASSIGNMENT,
                    TokenType.T_MOD_ASSIGNMENT,
                    TokenType.T_CONCAT_ASSIGNMENT))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseAssignment();
                return new ASTBinary(token, left, right);
            }
            return left;
        }
        private ASTNode ParseAddition()
        {
            ASTNode left = ParseMultiplication();
            while (IsMatch(TokenType.T_ADD, TokenType.T_SUB))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseMultiplication();
                left = new ASTBinary(token, left, right);
            }
            return left;
        }
        private ASTNode ParseMultiplication()
        {
            ASTNode left = ParsePow();
            while (IsMatch(TokenType.T_MUL, TokenType.T_DIV))
            {
                TokenItem token = NextToken();
                ASTNode right = ParsePow();
                left = new ASTBinary(token, left, right);
            }
            return left;
        }
        private ASTNode ParsePow()
        {
            ASTNode left = ParseObjectOperator();
            while (IsMatch(TokenType.T_POW))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseObjectOperator();
                left = new ASTBinary(token, left, right);
            }
            return left;
        }
        private ASTNode ParseObjectOperator()
        {
            if (IsMatch(TokenType.T_VARIABLE))
            {
                ASTNode left = new ASTData(NextToken(TokenType.T_VARIABLE));
                while (IsMatch(TokenType.T_OBJECT_OPERATOR, TokenType.T_NULLSAFE_OBJECT_OPERATOR))
                {
                    TokenItem token = NextToken();
                    ASTNode right;
                    if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
                    {
                        NextToken(TokenType.T_CURLY_BRACE_OPEN);
                        right = ParseAddition();
                        NextToken(TokenType.T_CURLY_BRACE_CLOSE);
                    }
                    else
                        right = new ASTData(NextToken(TokenType.T_STATIC_STRING));
                    left = new ASTBinary(token, left, right);
                }
                return left;
            }

            return ParseUnary();
        }
        private ASTNode ParseUnary()
        {
            if (IsMatch(TokenType.T_ADD, TokenType.T_SUB))
            {
                TokenItem token = NextToken();
                ASTNode expression = ParseUnary();
                return new ASTUnary(token, expression, ASTUnary.OperatorSide.Left);
            }
            return ParsePrimary();
        }
        private ASTNode ParsePrimary()
        {
            if (IsMatch(TokenType.T_BRACE_OPEN))
            {
                NextToken();
                ASTNode expression = ParseExpression();
                NextToken(TokenType.T_BRACE_CLOSE);
                return expression;
            }
            return new ASTData(NextToken(
                TokenType.T_LNUMBER,
                TokenType.T_DNUMBER, 
                TokenType.T_CONSTANT_ENCAPSED_STRING, 
                TokenType.T_VARIABLE,
                TokenType.T_STATIC_STRING));
        }
    }
}