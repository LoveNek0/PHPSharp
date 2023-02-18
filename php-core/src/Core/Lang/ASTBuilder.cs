﻿using PHP.Core.Exceptions;
using PHP.Core.Lang.AST.Nodes;
using PHP.Core.Lang.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang
{
    public class ASTBuilder
    {
        private TokenItem[] tokens;
        private int position;

        public ASTBuilder(TokenItem[] tokens)
        {
            List<TokenItem> tks = new List<TokenItem>();
            foreach(TokenItem item in tokens)
                switch (item.Type)
                {
                    default:
                        tks.Add(item);
                        break;
                    case TokenType.T_WHITESPACE:
                    case TokenType.T_COMMENT:
                    case TokenType.T_DOC_COMMENT:
                    case TokenType.T_OPEN_TAG:
                    case TokenType.T_CLOSE_TAG:
                        continue;
                }
            this.tokens = tks.ToArray();
            Reset();
        }

        public void Reset() => this.position = 0;

        private TokenItem Get(int offset = 0)
        {
            return position + offset >= tokens.Count() || position + offset < 0 ? null : tokens[position + offset];
        }
        private bool Match(params TokenType[] expected) =>
            expected.Length == 0 || expected.Contains(Get(0).Type);
        private TokenItem NextToken(params TokenType[] expected)
        {
            TokenItem item = Get(0);
            if(item == null)
                item = tokens[tokens.Length - 1];
            if(expected.Length > 0 && !expected.Contains(item.Type))
                throw new SyntaxException($"Unexpected token [{Get(0).Type}]", Get(0).Position);
            this.position++;
            return item;
        }

        private ASTFunctionNode ParseFunction()
        {
            position--;
            TokenItem token = NextToken(TokenType.T_FUNCTION);
            ASTFunctionNode node = new ASTFunctionNode(token);
            TokenItem next = NextToken(TokenType.T_STRING, TokenType.T_BRACE_OPEN);
            if (next.Type == TokenType.T_STRING)
            {
                node.Name = next;
                NextToken(TokenType.T_BRACE_OPEN);
            }
            while (true)
            {
                TokenItem type = null;
                TokenItem multi = null;
                TokenItem pointer = null;
                TokenItem name = null;
                TokenItem defaultValue = null;
                if (Match(TokenType.T_STRING))
                    type = NextToken(TokenType.T_STRING);
                if (Match(TokenType.T_ELLIPSIS))
                    multi = NextToken(TokenType.T_ELLIPSIS);
                if (Match(TokenType.T_BIT_AND))
                    pointer = NextToken(TokenType.T_BIT_AND);
                if (Match(TokenType.T_VARIABLE))
                    name = NextToken(TokenType.T_VARIABLE);
                //if (Match(TokenType.T_VARIABLE))
                //    name = NextToken(TokenType.T_VARIABLE);
                node.Arguments.Add(new ASTFunctionArgumentNode(type, multi, pointer, name, defaultValue));
                if (Match(TokenType.T_BRACE_CLOSE))
                    break;
                NextToken(TokenType.T_COMMA, TokenType.T_BRACE_CLOSE);
            }
            NextToken(TokenType.T_BRACE_CLOSE);
            if (Match(TokenType.T_USE))
            {
                NextToken(TokenType.T_USE);
                NextToken(TokenType.T_BRACE_OPEN);
                while (true)
                {
                    if (Match(TokenType.T_VARIABLE))
                        node.Use.Add(NextToken(TokenType.T_VARIABLE));
                    if (Match(TokenType.T_BRACE_CLOSE))
                        break;
                    NextToken(TokenType.T_COMMA, TokenType.T_BRACE_CLOSE);
                }
                NextToken(TokenType.T_BRACE_CLOSE);
            }
            NextToken(TokenType.T_CURLY_BRACE_OPEN);
            next = Get();
            while (next.Type != TokenType.T_CURLY_BRACE_CLOSE)
            {
                ASTNode n = NextNode();
                if (n != null)
                    node.Add(n);
                next = Get();
                continue;
            }
            NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            return node;
        }

        private ASTForNode ParseForLoop()
        {
            position--;
            ASTForNode node = new ASTForNode(NextToken(TokenType.T_FOR));
            NextToken(TokenType.T_BRACE_OPEN);
            bool conEnd = false;
            node.Initializer = NextNode(ref conEnd, 0, TokenType.T_SEMICOLON);
            conEnd = false;
            node.Condition = NextNode(ref conEnd, 0, TokenType.T_SEMICOLON);
            conEnd = false;
            node.Action = NextNode(ref conEnd, 0, TokenType.T_BRACE_CLOSE);

            if (Get(0).Type == TokenType.T_CURLY_BRACE_OPEN)
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);

                TokenItem next = Get();
                while (next.Type != TokenType.T_CURLY_BRACE_CLOSE)
                {
                    ASTNode n = NextNode();
                    if (n != null)
                        node.Add(n);
                    next = Get();
                    continue;
                }
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node.Add(NextNode());
            return node;
        }
        private ASTWhileNode ParseWhileLoop()
        {
            position--;
            ASTWhileNode node = new ASTWhileNode(NextToken(TokenType.T_WHILE));

            bool conEnd = false;
            NextToken(TokenType.T_BRACE_OPEN);
            node.condition = NextNode(ref conEnd, 0, TokenType.T_BRACE_CLOSE);

            if (Get(0).Type == TokenType.T_CURLY_BRACE_OPEN)
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);

                TokenItem next = Get();
                while (next.Type != TokenType.T_CURLY_BRACE_CLOSE)
                {
                    ASTNode n = NextNode();
                    if (n != null)
                        node.Add(n);
                    next = Get();
                    continue;
                }
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node.Add(NextNode());
            return node;
        }

        private ASTNode NextNode()
        {
            bool eol = false;
            return NextNode(ref eol);
        }

        private ASTNode NextNode(ref bool eol, uint deep = 0, TokenType endTokenType = TokenType.T_SEMICOLON, ASTNode prev = null)
        {
            if (eol)
                return prev;

            TokenItem token = Get(-1);
            if (prev == null)
                token = NextToken();
            else
                token = NextToken(token.Type.Info().Expected);

            if (token.Type == endTokenType)
            {
                eol = true;
                return prev;
            }

            switch (token.Type)
            {
                case TokenType.T_LNUMBER:
                case TokenType.T_DNUMBER:
                case TokenType.T_VARIABLE:
                    {
                        ASTDataNode node = new ASTDataNode(token);
                        if (prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_ADD:
                                case TokenType.T_SUB:
                                case TokenType.T_MUL:
                                case TokenType.T_DIV:
                                case TokenType.T_MOD:
                                case TokenType.T_POW:
                                case TokenType.T_CONCAT:
                                    return node;
                            }
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_ASSIGNMENT:
                case TokenType.T_ADD_ASSIGNMENT:
                case TokenType.T_SUB_ASSIGNMENT:
                case TokenType.T_MUL_ASSIGNMENT:
                case TokenType.T_DIV_ASSIGNMENT:
                case TokenType.T_MOD_ASSIGNMENT:
                case TokenType.T_POW_ASSIGNMENT:
                case TokenType.T_CONCAT_ASSIGNMENT:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_ADD:
                case TokenType.T_SUB:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_MUL:
                case TokenType.T_DIV:
                case TokenType.T_MOD:
                case TokenType.T_POW:
                case TokenType.T_CONCAT:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(token, deep);
                        if (prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_ADD:
                                case TokenType.T_SUB:
                                    if (((ASTBinaryNode)prev).deep == deep)
                                    {
                                        node.left = ((ASTBinaryNode)prev).right;
                                        ((ASTBinaryNode)prev).right = node;
                                        node.right = NextNode(ref eol, deep, endTokenType, node);
                                        return NextNode(ref eol, deep, endTokenType, prev);
                                    }
                                    break;
                            }
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_BRACE_OPEN:
                    {
                        bool e = false;
                        ASTNode node = NextNode(ref e, deep + 1, TokenType.T_BRACE_CLOSE);
                        if (prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_ADD:
                                case TokenType.T_SUB:
                                case TokenType.T_MUL:
                                case TokenType.T_DIV:
                                case TokenType.T_MOD:
                                    return node;
                            }
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_ECHO:
                case TokenType.T_RETURN:
                    {
                        ASTUnaryNode node = new ASTUnaryNode(token);
                        node.operand = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_INCREMENT:
                    {
                        ASTUnaryNode node = new ASTUnaryNode(token);
                        if(prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_VARIABLE:
                                    node.operand = prev;
                                    node.type = ASTUnaryNode.UnaryOperatorType.Postfix;
                                    return NextNode(ref eol, deep, endTokenType, node);
                            }
                        node.type = ASTUnaryNode.UnaryOperatorType.Prefix;
                        node.operand = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_CURLY_BRACE_OPEN:
                    {
                        ASTListNode node = new ASTListNode(token);

                        TokenItem next = Get(0);
                        while (next.Type != TokenType.T_CURLY_BRACE_CLOSE)
                        {
                            ASTNode n = NextNode();
                            //if (n != null)
                                node.Add(n);
                            next = Get(0);
                            continue;
                        }
                        NextToken(TokenType.T_CURLY_BRACE_CLOSE);
                        return node;
                    }
                case TokenType.T_WHILE:
                    return ParseWhileLoop();
                case TokenType.T_FOR:
                    return ParseForLoop();
                case TokenType.T_FUNCTION:
                    return ParseFunction();
            }

            throw new SyntaxException($"Incorrect token {token.Type}", token.Position);
        }

        public ASTRootNode Build()
        {
            ASTRootNode node = new ASTRootNode();
            while(Get(0).Type != TokenType.T_EOF)
            {
                ASTNode next = NextNode();
                if (next != null)
                    node.Add(next);
            }
            return node;
        } 
    }
}