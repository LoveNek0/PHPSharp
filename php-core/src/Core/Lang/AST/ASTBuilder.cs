using PHP.Core.Exceptions;
using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.AST.Nodes.BinaryExpression;
using PHP.Core.Lang.AST.Nodes.Data;
using PHP.Core.Lang.AST.Nodes.Loop;
using PHP.Core.Lang.AST.Nodes.Structure;
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
        private int position;

        public ASTBuilder(TokenItem[] tokens)
        {
            this.tokens = tokens;
            Reset();
        }

        public void Reset() => this.position = 0;

        private TokenItem PeekToken()
        {
            if (position >= tokens.Count())
                return null;
            return tokens[position];
        }
        private TokenItem PeekToken(params TokenType[] expected)
        {
            TokenItem next = PeekToken();
            if(expected.Contains(next.Type))
                return next;
            throw new SyntaxException("Unexpected token " + next.Type, next.Position.Index, next.Position.Line, next.Position.Column);
        }

        private TokenItem NextToken()
        {
            TokenItem next = PeekToken();
            position++;
            return next;
        }
        private TokenItem NextToken(params TokenType[] expected)
        {
            TokenItem next = PeekToken(expected);
            position++;
            return next;
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

            TokenItem token = NextToken();

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
                                case TokenType.T_CONCAT:
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
                                case TokenType.T_CONCAT:
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
                                case TokenType.T_CONCAT:
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
                case TokenType.T_ADD_ASSIGNMENT:
                    {
                        ASTAddAssignmentNode node = new ASTAddAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_SUB_ASSIGNMENT:
                    {
                        ASTSubAssignmentNode node = new ASTSubAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_MUL_ASSIGNMENT:
                    {
                        ASTMulAssignmentNode node = new ASTMulAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_DIV_ASSIGNMENT:
                    {
                        ASTDivAssignmentNode node = new ASTDivAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_MOD_ASSIGNMENT:
                    {
                        ASTModAssignmentNode node = new ASTModAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_POW_ASSIGNMENT:
                    {
                        ASTPowAssignmentNode node = new ASTPowAssignmentNode(token, deep);
                        node.left = prev;
                        node.right = NextNode(ref eol, deep, endTokenType, node);
                        return node;
                    }
                case TokenType.T_CONCAT_ASSIGNMENT:
                    {
                        ASTConcatAssignmentNode node = new ASTConcatAssignmentNode(token, deep);
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
                case TokenType.T_CONCAT:
                    {
                        ASTPowNode node = new ASTPowNode(token, deep);
                        if (prev != null)
                            switch (prev.Token.Type)
                            {
                                case TokenType.T_ADD:
                                case TokenType.T_SUB:
                                case TokenType.T_MUL:
                                case TokenType.T_DIV:
                                case TokenType.T_MOD:
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
                case TokenType.T_WHILE:
                    {
                        ASTWhileNode node = new ASTWhileNode(token);

                        bool conEnd = false;
                        NextToken(TokenType.T_BRACE_OPEN);
                        node.condition = NextNode(ref conEnd, 0, TokenType.T_BRACE_CLOSE);
                        
                        NextToken(TokenType.T_CURLY_BRACE_OPEN);

                        TokenItem next = PeekToken();
                        while(next.Type != TokenType.T_CURLY_BRACE_CLOSE)
                        {
                            ASTNode n = NextNode();
                            if (n != null)
                                node.Add(n);
                            next = PeekToken();
                        }
                        return node;
                    }
            }
            return null;
        }

        public ASTRootNode Build()
        {
            ASTRootNode node = new ASTRootNode();
            while(position < tokens.Length)
            {
                ASTNode next = NextNode();
                if (next != null)
                    node.Add(next);
            }
            return node;
        } 
    }
}
