using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.AST.Nodes.BinaryExpression;
using PHP.Core.Lang.AST.Nodes.Data;
using PHP.Core.Lang.AST.Nodes.UnaryExpression;
using PHP.Core.Lang.Token;
using PHP.Core.Lang.Token.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTBuilder
    {
        private TokenItem[] tokens;
        private uint position;

        public ASTBuilder(TokenItem[] tokens)
        {
            this.tokens = tokens;
            Reset();
        }

        public void Reset()
        {
            this.position = 0;
        }

        public ASTNode NextNode()
        {
            bool eol = false;
            return NextNode(ref eol);
        }
        public ASTNode NextNode(ref bool eol, uint deep = 0, TokenType endTokenType = TokenType.T_SEMICOLON, ASTNode prev = null)
        {
            if (eol)
                return prev;
        re:
            TokenItem token = tokens[position++];
            if (token.Type.Info().Family == TokenFamily.Ignore)
                goto re;

            if (token.Type == endTokenType)
            {
                eol = true;
                return prev;
            }
            switch (token.Type)
            {
                case TokenType.T_LNUMBER:
                    {
                        ASTLNumberNode node = new ASTLNumberNode(token);
                        if (prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_ADD:
                                case TokenType.T_SUB:
                                case TokenType.T_MUL:
                                case TokenType.T_DIV:
                                case TokenType.T_MOD:
                                case TokenType.T_POW:
                                    return node;
                            }
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_DNUMBER:
                    {
                        ASTLNumberNode node = new ASTLNumberNode(token);
                        if (prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_ADD:
                                case TokenType.T_SUB:
                                case TokenType.T_MUL:
                                case TokenType.T_DIV:
                                case TokenType.T_MOD:
                                case TokenType.T_POW:
                                    return node;
                            }
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_VARIABLE:
                    {
                        ASTVariableNode node = new ASTVariableNode(token);
                        if (prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_ADD:
                                case TokenType.T_SUB:
                                case TokenType.T_MUL:
                                case TokenType.T_DIV:
                                case TokenType.T_MOD:
                                case TokenType.T_POW:
                                    return node;
                            }
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_ASSIGNMENT:
                    {
                        ASTAssignmentNode node = new ASTAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_ADD:
                    {
                        ASTAssignmentNode node = new ASTAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_SUB:
                    {
                        ASTAssignmentNode node = new ASTAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return NextNode(ref eol, deep, endTokenType, node);
                    }
                case TokenType.T_MUL:
                    {
                        ASTMulNode node = new ASTMulNode(token, deep);
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
                case TokenType.T_DIV:
                    {
                        ASTMulNode node = new ASTMulNode(token, deep);
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
                case TokenType.T_MOD:
                    {
                        ASTModNode node = new ASTModNode(token, deep);
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
                case TokenType.T_POW:
                    {
                        ASTPowNode node = new ASTPowNode(token, deep);
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
                    {
                        ASTEchoNode node = new ASTEchoNode(token);
                        node.operand = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
            }
            return null;
        }
    }
}
