using PHP.Core.Lang.Lexic.Token;
using PHP.Core.Exceptions;
using PHP.Core.Lang.Syntax.AST;
using PHP.Core.Lang.Syntax.AST.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang.Syntax.AST.Basic;

namespace PHP.Core.Lang.Syntax
{
    public class Syntaxer
    {
        private TokenItem[] tokens;
        private int current = 0;
        private TokenItem currentNamespace = null;


        public Syntaxer(TokenItem[] tokens) => this.tokens = tokens;

        private TokenItem NextItem(params TokenType[] expected)
        {
            if (current >= tokens.Length)
                return null;
            if(expected == null || expected.Length == 0)
                return tokens[current++];
            if(expected.Contains(tokens[current].type))
                return tokens[current++];
            string message = "Unexpected " + tokens[current].type.ToString() + " \"" + tokens[current].data + "\", expected " + expected[0];
            if (expected.Length > 1)
            {
                for (int i = 1; i < expected.Length - 1; i++)
                    message += ", " + expected[i];
                message += " or " + expected[expected.Length - 1];
            }
            throw new SyntaxException(message, tokens[current].position, tokens[current].line, tokens[current].column);
        }

        private ASTNode ParseNext(ref bool endOfLine, ASTNode prev = null, params TokenType[] expected)
        {
            if (current >= tokens.Length)
                endOfLine = true;
            if (endOfLine)
                return prev;
            TokenItem next = NextItem(expected);
            switch (next.type)
            {
                case TokenType.T_NAMESPACE:
                    {
                        TokenItem nextNS = NextItem();
                        if (nextNS.type == TokenType.T_NAMESPACE_NAME || nextNS.type == TokenType.T_STRING)
                            this.currentNamespace = nextNS;
                        else
                            this.currentNamespace = null;
                        return ParseNext(ref endOfLine);
                    }
                case TokenType.T_ECHO:
                    {
                        ASTUnaryNode node = new ASTUnaryNode(next);
                        node.operand = ParseNext(ref endOfLine, node);
                        return ParseNext(ref endOfLine, node);
                    }
                case TokenType.T_INLINE_HTML:
                    {
                        ASTNode node = new ASTContainerNode(next);
                        node.parent = prev;
                        if (prev == null)
                            return node;
                        return ParseNext(ref endOfLine, node);
                    }
                case TokenType.T_VARIABLE:
                case TokenType.T_DNUMBER:
                case TokenType.T_LNUMBER:
                    {
                        ASTNode node = new ASTContainerNode(next);
                        node.parent = prev;
                        return ParseNext(ref endOfLine, node);
                    }
                case TokenType.T_EQUAL:
                case TokenType.T_MUL:
                case TokenType.T_DIV:
                case TokenType.T_MOD:
                case TokenType.T_POW:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(next, prev);
                        node.right = ParseNext(ref endOfLine, node);
                        node.left.parent = node;
                        node.right.parent = node;
                        return node;
                    }
                case TokenType.T_ADD:
                case TokenType.T_SUB:
                    {
                        
                        if (prev != null && prev.parent != null)
                            if (prev.parent.GetType() == typeof(ASTBinaryNode))
                                if (((ASTBinaryNode)prev.parent).token.type == TokenType.T_MUL || ((ASTBinaryNode)prev.parent).token.type == TokenType.T_DIV || ((ASTBinaryNode)prev.parent).token.type == TokenType.T_MOD || ((ASTBinaryNode)prev.parent).token.type == TokenType.T_POW)
                                {
                                    current--;
                                    return prev;
                                }
                        ASTBinaryNode node = new ASTBinaryNode(next, prev);
                        node.right = ParseNext(ref endOfLine, node);
                        node.left.parent = node;
                        node.right.parent = node;
                        return ParseNext(ref endOfLine, node);
                    }
                case TokenType.T_BRACE_OPEN:
                    return ParseNext(ref endOfLine);
                case TokenType.T_SEMICOLON:
                    endOfLine = true;
                    return prev;
                case TokenType.T_BRACE_CLOSE:
                    return prev;
            }
            return null;
        }

        public ASTListNode Parse()
        {
            ASTListNode result = new ASTListNode();
            while (current < tokens.Length)
            {
                bool e = false;
                ASTNode node = ParseNext(ref e);
                if(node != null)
                    result.AddNode(node);
            }
            return result;
        }
    }
}
