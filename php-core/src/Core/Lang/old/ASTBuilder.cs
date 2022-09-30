using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PHP.Core.Exceptions;
using PHP.Core.Lang.AST;
using PHP.Core.Lang.AST.Nodes;
using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using PHP.Core.Lang.Token.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang.AST.Parsers;

namespace PHP.Core.Lang
{
    public class ASTBuilder
    {
        private TokenItem[] tokens;
        private int position;

        public ASTBuilder(TokenItem[] tokens)
        {
            this.tokens = tokens;
            this.position = 0;
        }

        public TokenItem NextToken(params TokenType[] expected)
        {
            if(position >= tokens.Length)
                return null;
            TokenItem item = tokens[position];
            if(item.Type.Info().Family == TokenFamily.Ignore)
            {
                position++;
                return NextToken(expected);
            }
            if (expected.Contains(item.Type))
            {
                position++;
                return item;
            }
            throw new SyntaxException("Unexpected token \"" + item.Type + "\"", item.Position.Index, item.Position.Line, item.Position.Column);
        }
        public TokenItem NextToken(TokenInfo info) => NextToken(info.Expected);
        public TokenItem NextToken(TokenItem token) => NextToken(token.Type.Info());

        private TokenItem GetToken(int index)
        {
            if (index >= tokens.Length)
                return null;
            return tokens[index];
        }

        private ASTNode NextNode()
        {
            TokenItem token = GetToken(position);
            switch (token.Type.Info().Family)
            {
                case TokenFamily.EndOfLine:
                case TokenFamily.Ignore:
                    position++;
                    return null;
                case TokenFamily.UnaryOperator:
                case TokenFamily.Brace:
                case TokenFamily.Data:
                    return (new ExpressionParser(this)).Parse();
                case TokenFamily.Loop:
                    switch (token.Type)
                    {
                        case TokenType.T_WHILE:
                            {
                                TokenItem item = NextToken(TokenType.T_WHILE);
                                ASTListNode list = new ASTListNode(item);
                                item = NextToken(item);
                                TokenItem open = item;
                                while(true)
                                {
                                    item = NextToken(open);
                                    if (item.Type == TokenType.T_CURLY_BRACE_CLOSE)
                                        return list;
                                    position--;
                                    ASTNode node = NextNode();
                                    if(node != null)
                                        list.Add(node);
                                }
                            }
                            break;
                    }
                    break;
            }
            return null;
        }

        public ASTRootNode Build()
        {
            ASTRootNode root = new ASTRootNode();
            while (position < tokens.Length)
            {
                ASTNode node = NextNode();
                if(node != null)
                    root.Add(node);
            }
            return root;
        }

    }
}
