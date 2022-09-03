using PHP.Core.Exceptions;
using PHP.Core.Lang.AST;
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
            TokenType[] toRemove =
            {
                TokenType.T_OPEN_TAG,
                TokenType.T_CLOSE_TAG,
                TokenType.T_INLINE_HTML,
                TokenType.T_WHITESPACE
            };
            List<TokenItem> list = new List<TokenItem>();
            foreach(TokenItem item in tokens)
                if(!toRemove.Contains(item.type))
                    list.Add(item);
            this.tokens = list.ToArray();
            this.position = 0;
        }

        private TokenItem NextToken(params TokenType[] expected)
        {
            if(position >= tokens.Length)
                return null;
            TokenItem item = tokens[position];
            if (expected.Contains(item.type))
            {
                position++;
                return item;
            }
            throw new SyntaxException("Unexpected token \"" + item.type + "\"", item.position, item.line, item.column);
        }
        private TokenItem GetToken(int index)
        {
            if (index >= tokens.Length)
                return null;
            return tokens[index];
        }
        private ASTNode NextNode(TokenItem token, ASTNode parent = null)
        {
            if (token == null)
                return null;
            switch (token.type)
            {
                case TokenType.T_INLINE_HTML:
                    return new ASTNode(token);
                case TokenType.T_VARIABLE:
                    return NextNode(NextToken(token.type.GetNextExpected()), new ASTNode(token));
                case TokenType.T_ADD:
                case TokenType.T_SUB:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(token, parent);
                        ASTNode right = NextNode(NextToken(token.type.GetNextExpected()), node);
                        node.rightOperand = right;
                        return NextNode(NextToken(TokenType.T_LNUMBER.GetNextExpected()), node);
                    }
                case TokenType.T_MUL:
                case TokenType.T_DIV:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(token, parent);
                        ASTNode right = NextNode(NextToken(token.type.GetNextExpected()), node);
                        node.rightOperand = right;
                        return node;
                    }
                case TokenType.T_ASSIGNMENT:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(token, parent);
                        node.rightOperand = NextNode(NextToken(token.type.GetNextExpected()));
                        return node;
                    }
                case TokenType.T_LNUMBER:
                    {
                        ASTNode node = new ASTNode(token);
                        TokenItem nextItem = GetToken(position + 1);
                        if (parent == null
                            ||
                            (nextItem != null && (nextItem.type != TokenType.T_MUL || nextItem.type != TokenType.T_DIV)))
                            return NextNode(NextToken(token.type.GetNextExpected()), node);
                        return node;
                    }
                case TokenType.T_SEMICOLON:
                    return parent;
            }
            return null;
        }

        public ASTFile Build()
        {
            ASTFile file = new ASTFile();
            while(position < tokens.Length)
            {
                TokenItem token = NextToken(TokenType.T_INLINE_HTML, TokenType.T_VARIABLE, TokenType.T_ECHO);
                ASTNode node = NextNode(token);
                if (node != null)
                    file.Add(node);
            }
            return file;
        }
    }
}
