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
            TokenType.T_VARIABLE
        };
        private readonly TokenType[] variables =
        {
            TokenType.T_VARIABLE
        };

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
            return null;
        }

        public ASTFunction ParseFunction()
        {
            TokenItem token = NextToken(TokenType.T_FUNCTION);
            ASTFunction func = new ASTFunction(token);
            if (Match(TokenType.T_STRING))
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
                block._lines.Add(ParseLine());
            }
            func._block = block;
            return func;
        }
    }
}