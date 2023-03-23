using PHP.Core.Lang.AST;
using PHP.Core.Lang.Exceptions;
using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang
{
    public class Parser
    {
        private readonly TokenType[] ignore =
        {
            TokenType.T_OPEN_TAG,
            TokenType.T_CLOSE_TAG,
            TokenType.T_WHITESPACE,
            TokenType.T_COMMENT
        };
        private readonly TokenType[] leftAssociative =
        {
            TokenType.T_ADD,
            TokenType.T_SUB,
            TokenType.T_MUL,
            TokenType.T_DIV,
            TokenType.T_MOD,
            TokenType.T_POW
        };
        private readonly TokenType[] rightAssociative =
        {
            TokenType.T_ASSIGNMENT,
            TokenType.T_ADD_ASSIGNMENT,
            TokenType.T_SUB_ASSIGNMENT,
            TokenType.T_MUL_ASSIGNMENT,
            TokenType.T_DIV_ASSIGNMENT,
            TokenType.T_POW_ASSIGNMENT,
            TokenType.T_MOD_ASSIGNMENT
        };
        private readonly TokenType[] binaryOperators =
        {
            TokenType.T_ADD,
            TokenType.T_SUB,
            TokenType.T_MUL,
            TokenType.T_DIV,
            TokenType.T_MOD,
            TokenType.T_POW,
            TokenType.T_ASSIGNMENT,
            TokenType.T_ADD_ASSIGNMENT,
            TokenType.T_SUB_ASSIGNMENT,
            TokenType.T_MUL_ASSIGNMENT,
            TokenType.T_DIV_ASSIGNMENT,
            TokenType.T_POW_ASSIGNMENT,
            TokenType.T_MOD_ASSIGNMENT
        };
        private readonly TokenType[] unaryOperators =
        {
            TokenType.T_ADD,
            TokenType.T_SUB,
            TokenType.T_INCREMENT,
            TokenType.T_DECREMENT
        };
        private readonly TokenType[] leftUnaryOperators =
        {
            TokenType.T_ADD,
            TokenType.T_SUB,
            TokenType.T_INCREMENT,
            TokenType.T_DECREMENT
        };
        private readonly TokenType[] rightUnaryOperators =
        {
            TokenType.T_INCREMENT,
            TokenType.T_DECREMENT
        };
        private readonly TokenType[] data =
        {
            TokenType.T_LNUMBER,
            TokenType.T_DNUMBER,
            TokenType.T_VARIABLE,
            TokenType.T_CONSTANT_ENCAPSED_STRING
        };
        private readonly TokenType[] lineStart =
        {
            TokenType.T_VARIABLE,
            TokenType.T_FUNCTION
        };
        private readonly TokenType[] variables =
        {
            TokenType.T_VARIABLE
        };
        private readonly uint maxPriority = 5;

        private TokenItem[] tokens;
        private int index;

        public Parser(TokenItem[] tokens)
        {
            this.tokens = (from TokenItem item in tokens where !ignore.Contains(item.Type) select item).ToArray();
            this.index = 0;
        }

        public uint GetPriority(TokenType type)
        {
            switch (type)
            {
                case TokenType.T_ADD:
                case TokenType.T_SUB:
                    return 1;
                case TokenType.T_MUL:
                case TokenType.T_DIV:
                    return 2;
                case TokenType.T_POW:
                case TokenType.T_MOD:
                    return 3;
                case TokenType.T_OBJECT_OPERATOR:
                    return 4;
                case TokenType.T_DOUBLE_COLON:
                    return 5;
                default:
                    return 0;
            }
        }

        public TokenItem Get(int offset = 0, params TokenType[] expected)
        {
            if (index + offset < 0 || index + offset >= tokens.Length)
                return null;
            TokenItem token = tokens[index];
            if (expected.Length == 0)
                return token;
            if (expected.Contains(token.Type))
                return token;
            throw new SyntaxException($"Unexpected token {token.Type}, expected {String.Join(",", expected)}", token.Position);
        }
        public TokenItem Get(params TokenType[] expected) => Get(0, expected);
        public bool Match(int offset = 0, params TokenType[] expected)
        {
            TokenItem token = Get(offset);
            if (token == null)
                return false;
            return expected.Contains(token.Type);
        }
        public bool Match(params TokenType[] expected) => Match(0, expected); 
        public TokenItem NextToken(params TokenType[] expected)
        {
            TokenItem i = Get(expected);
            index++;
            return i;
        }

        public ASTNode NextNode()
        {
            bool e = false;
            return NextNode(ref e, 0, 0, null, TokenType.T_SEMICOLON);
        }
        public ASTNode NextNode(ref bool endOfLine, uint deep, uint prevDeep, ASTNode prev, TokenType endTokenType)
        {
            if (endOfLine)
                return prev;

            TokenItem token = Get();
            if(token.Type == endTokenType)
            {
                index++;
                endOfLine = true;
                return prev;
            }
            if (data.Contains(token.Type))
            {
                ASTData node = new ASTData(token);
                index++;
                if (prev != null && leftAssociative.Contains(prev.Token.Type))
                    return node;
                return NextNode(ref endOfLine, deep, deep, node, endTokenType);
            }
            if (binaryOperators.Contains(token.Type))
            {
                index++;
                ASTBinary node = new ASTBinary(token);
                if(prev != null && leftAssociative.Contains(prev.Token.Type))
                    if(GetPriority(prev.Token.Type) < GetPriority(token.Type))
                        if(prevDeep == deep)
                        {
                            node._left = ((ASTBinary)prev)._right;
                            ((ASTBinary)prev)._right = node;
                            node._right = NextNode(ref endOfLine, deep, deep, node, endTokenType);
                            return NextNode(ref endOfLine, deep, deep, prev, endTokenType);
                        }
                node._left = prev;
                node._right = NextNode(ref endOfLine, deep, deep, node, endTokenType);
                if(leftAssociative.Contains(token.Type))
                    return NextNode(ref endOfLine, deep, deep, node, endTokenType);
                if (rightAssociative.Contains(token.Type))
                    return node;
            }
            if (token.Type == TokenType.T_BRACE_OPEN)
            {
                index++;
                bool e = false;
                ASTNode node = NextNode(ref e, deep + 1, deep, null, TokenType.T_BRACE_CLOSE);
                if (prev != null && leftAssociative.Contains(prev.Token.Type))
                    return node;
                return NextNode(ref endOfLine, deep, deep, node, endTokenType);
            }
            throw new SyntaxException($"Unexpected token \"{token.Type}\"", token.Position);
        }

        public ASTNode ParseLine()
        {
            switch (Get(lineStart).Type)
            {
                case TokenType.T_FUNCTION:
                    return ParseFunction();
                default:
                    {
                        bool eol = false;
                        return ParseExpression(ref eol, 0, 0, null, TokenType.T_SEMICOLON);
                    }
            }
        }
        public List<TokenItem> RPN()
        {
            Stack<TokenItem> stack = new Stack<TokenItem>();
            List<TokenItem> result = new List<TokenItem>();

            while(true)
            {
                TokenItem token = NextToken();
                if (token.Type == TokenType.T_SEMICOLON)
                    break;
                if (data.Contains(token.Type))
                    result.Add(token);
                if (token.Type == TokenType.T_BRACE_OPEN)
                    stack.Push(token);
                if(token.Type == TokenType.T_BRACE_CLOSE)
                {
                    while (stack.Count() > 0 && stack.Peek().Type != TokenType.T_BRACE_OPEN)
                        result.Add(stack.Pop());
                    stack.Pop();
                }
                if (binaryOperators.Contains(token.Type))
                {
                    while ((stack.Count() > 0 && binaryOperators.Contains(stack.Peek().Type))
                        &&
                        (
                            (GetPriority(token.Type) <= GetPriority(stack.Peek().Type))
                            ||
                            (
                                leftAssociative.Contains(stack.Peek().Type)
                                &&
                                GetPriority(token.Type) == GetPriority(stack.Peek().Type)
                            )
                        )
                    )
                        result.Add(stack.Pop());
                    stack.Push(token);
                }
            }
            while(stack.Count() > 0)
                result.Add(stack.Pop());
            return result;
        }
        public ASTNode ParseExpression(ref bool eol, uint nowdeep, uint prevdeep, ASTNode prev, TokenType eolt)
        {
            if (eol)
                return prev;
            if (Match(eolt))
            {
                NextToken(eolt);
                eol = true;
                return prev;
            }
            if (Match(data))
            {
                ASTNode node = ParseData();
                if (prev != null && leftAssociative.Contains(prev.Token.Type))
                    return node;
                return ParseExpression(ref eol, nowdeep, prevdeep, node, eolt);
            }
            if (Match(binaryOperators))
            {
                ASTBinary node = ParseBinary() as ASTBinary;
                node._left = prev;
                node._right = ParseExpression(ref eol, nowdeep, nowdeep, node, eolt);
                if (prev != null)
                {
                    if (binaryOperators.Contains(prev.Token.Type))
                        if(nowdeep == prevdeep)
                            if (GetPriority(prev.Token.Type) < GetPriority(node.Token.Type))
                            {
                                node._left = ((ASTBinary)prev)._right;
                                ((ASTBinary)prev)._right = node;
                                return ParseExpression(ref eol, nowdeep, nowdeep, prev, eolt);
                            }
                }
                if (leftAssociative.Contains(node.Token.Type))
                    return ParseExpression(ref eol, nowdeep, nowdeep, node, eolt);
                if (rightAssociative.Contains(node.Token.Type))
                    return node;
            }
            if (Match(TokenType.T_BRACE_OPEN))
            {
                NextToken(TokenType.T_BRACE_OPEN);
                bool e = false;
                ASTNode node = ParseExpression(ref e, nowdeep + 1, nowdeep, null, TokenType.T_BRACE_CLOSE);
                if (prev != null && leftAssociative.Contains(prev.Token.Type))
                    return node;
                return ParseExpression(ref eol, nowdeep, nowdeep + 1, node, eolt);
            }
            TokenItem nowToken = Get();
            throw new SyntaxException($"Unexpected token \"{nowToken.Type}\"", nowToken.Position);
        }
        public ASTNode ParseData()
        {
            return new ASTData(NextToken(data));
        }
        public ASTNode ParseBinary()
        {
            return new ASTBinary(NextToken(binaryOperators));
        }
        public ASTNode ParseVariable()
        {
            ASTNode node = new ASTData(NextToken(TokenType.T_VARIABLE));
            while (Match(TokenType.T_OBJECT_OPERATOR))
            {
                ASTBinary binary = new ASTBinary(NextToken(TokenType.T_OBJECT_OPERATOR));
                binary._left = node;
                binary._right = new ASTData(NextToken(TokenType.T_STRING));
                node = binary;
            }
            return node;
        }
        public ASTNode ParseFunction()
        {
            TokenItem token = NextToken(TokenType.T_FUNCTION);
            ASTFunction func = new ASTFunction(token);
            func._name = NextToken(TokenType.T_STRING);
            NextToken(TokenType.T_BRACE_OPEN);
            while (true)
            {
                if (Match(TokenType.T_COMMA))
                    NextToken(TokenType.T_COMMA);
                else
                    if (Match(TokenType.T_BRACE_CLOSE))
                    {
                        NextToken(TokenType.T_BRACE_CLOSE);
                        break;
                    }
                TokenItem type = null;
                if(Match(TokenType.T_STRING))
                    type = NextToken(TokenType.T_STRING);
                token = NextToken(TokenType.T_VARIABLE);
                ASTFunctionArgument argument = new ASTFunctionArgument(token);
                argument._type = type;
                func._arguments.Add(argument);
            }
            ASTBlock block = new ASTBlock(NextToken(TokenType.T_CURLY_BRACE_OPEN));
            while (true)
            {
                if (Match(TokenType.T_CURLY_BRACE_CLOSE))
                {
                    NextToken(TokenType.T_CURLY_BRACE_CLOSE);
                    break;
                }
                //block._lines.Add(ParseLine());
            }
            func._block = block;
            return func;
        }
        public ASTNode ParseLambda()
        {
            TokenItem token = NextToken(TokenType.T_FUNCTION);
            ASTLambda func = new ASTLambda(token);
            NextToken(TokenType.T_BRACE_OPEN);
            while (true)
            {
                if (Match(TokenType.T_COMMA))
                    NextToken(TokenType.T_COMMA);
                else
                    if (Match(TokenType.T_BRACE_CLOSE))
                {
                    NextToken(TokenType.T_BRACE_CLOSE);
                    break;
                }
                TokenItem type = null;
                if (Match(TokenType.T_STRING))
                    type = NextToken(TokenType.T_STRING);
                token = NextToken(TokenType.T_VARIABLE);
                ASTFunctionArgument argument = new ASTFunctionArgument(token);
                argument._type = type;
                func._arguments.Add(argument);
            }
            if (Match(TokenType.T_USE))
            {
                NextToken(TokenType.T_USE);
                NextToken(TokenType.T_BRACE_OPEN);
                while (true)
                {
                    if (Match(TokenType.T_COMMA))
                        NextToken(TokenType.T_COMMA);
                    else
                        if (Match(TokenType.T_BRACE_CLOSE))
                            break;
                    func._use.Add(NextToken(TokenType.T_VARIABLE));
                }
                NextToken(TokenType.T_BRACE_CLOSE);
            }
            ASTBlock block = new ASTBlock(NextToken(TokenType.T_CURLY_BRACE_OPEN));
            while (true)
            {
                if (Match(TokenType.T_CURLY_BRACE_CLOSE))
                {
                    NextToken(TokenType.T_CURLY_BRACE_CLOSE);
                    break;
                }
                //block._lines.Add(ParseLine());
            }
            func._block = block;
            return func;
        }
    }
}