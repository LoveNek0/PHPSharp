using PHP.Core.Lang.AST.Nodes.Basic;
using PHP.Core.Lang.Token;
using PHP.Core.Lang.Token.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Parsers
{
    public class ExpressionParser
    {
        private readonly ASTBuilder builder;

        public ExpressionParser(ASTBuilder builder) => this.builder = builder;

        private bool CompareOperators(TokenInfo op1, TokenInfo op2) =>
            op1.Side == TokenSide.Right ? op1.Priority < op2.Priority : op1.Priority <= op2.Priority;

        private IEnumerable<TokenItem> ShuntingYard()
        {
            var stack = new Stack<TokenItem>();
            TokenItem token = builder.NextToken(
            TokenType.T_VARIABLE
            );
            while (token.Type != TokenType.T_SEMICOLON)
            {
                switch (token.Type.Info().Family)
                {
                    case TokenFamily.Data:
                        yield return token;
                        break;
                    case TokenFamily.Separator:
                        while (stack.Peek().Type != TokenType.T_BRACE_OPEN)
                            yield return stack.Pop();
                        break;
                    case TokenFamily.BinaryOparator:
                        while (stack.Any() && stack.Peek().Type.Info().Family == TokenFamily.BinaryOparator && CompareOperators(token.Type.Info(), stack.Peek().Type.Info()))
                            yield return stack.Pop();
                        stack.Push(token);
                        break;
                    case TokenFamily.Brace:
                        switch (token.Type)
                        {
                            case TokenType.T_BRACE_OPEN:
                                stack.Push(token);
                                break;
                            case TokenType.T_BRACE_CLOSE:
                                while (stack.Peek().Type != TokenType.T_BRACE_OPEN)
                                    yield return stack.Pop();
                                stack.Pop();
                                if (stack.Peek().Type == TokenType.T_FUNCTION)
                                    yield return stack.Pop();
                                break;
                        }
                        break;
                }
                /*
                switch (token.Type)
                {
                    case TokenType.T_LNUMBER:
                    case TokenType.T_VARIABLE:
                        yield return token;
                        break;
                    case TokenType.T_FUNCTION:
                        stack.Push(token);
                        break;
                    case TokenType.T_COMMA:
                        while (stack.Peek().Type != TokenType.T_BRACE_OPEN)
                            yield return stack.Pop();
                        break;
                    case TokenType.T_ADD:
                    case TokenType.T_SUB:
                    case TokenType.T_MUL:
                    case TokenType.T_DIV:
                    case TokenType.T_MOD:
                    case TokenType.T_POW:
                    case TokenType.T_ASSIGNMENT:
                        while (stack.Any() && stack.Peek().Type.Info().Family == TokenFamily.BinaryOparator && CompareOperators(token.Type.Info(), stack.Peek().Type.Info()))
                            yield return stack.Pop();
                        stack.Push(token);
                        break;
                    case TokenType.T_BRACE_OPEN:
                        stack.Push(token);
                        break;
                    case TokenType.T_BRACE_CLOSE:
                        while (stack.Peek().Type != TokenType.T_BRACE_OPEN)
                            yield return stack.Pop();
                        stack.Pop();
                        if (stack.Peek().Type == TokenType.T_FUNCTION)
                            yield return stack.Pop();
                        break;
                    default:
                        throw new Exception("Wrong token");
                }
                */
                token = builder.NextToken(token);
            }
            while (stack.Any())
            {
                var tok = stack.Pop();
                if (tok.Type == TokenType.T_BRACE_OPEN || tok.Type == TokenType.T_BRACE_CLOSE)
                    throw new Exception("Mismatched parentheses");
                yield return tok;
            }
        }

        private ASTNode BuildNode(ref List<TokenItem> tokens)
        {
            if (tokens.Count() == 0)
                return null;
            TokenItem token = tokens[tokens.Count() - 1];
            tokens.RemoveAt(tokens.Count() - 1);
            switch (token.Type.Info().Family)
            {
                case TokenFamily.Data:
                    return new ASTNode(token);
                case TokenFamily.BinaryOparator:
                    {
                        ASTBinaryNode node = new ASTBinaryNode(token);
                        node.rightOperand = BuildNode(ref tokens);
                        node.leftOperand = BuildNode(ref tokens);
                        return node;
                    }
            }
            return null;
        }

        public ASTNode Parse()
        {
            List<TokenItem> tokens = new List<TokenItem>(ShuntingYard());
            return BuildNode(ref tokens);
        }

    }
}
