using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Loops;

public class ASTWhile : ASTBlock
{
    public ASTNode Condition => _condition;
    internal ASTNode _condition;
    internal ASTWhile(TokenItem token) : base(token)
    {
    }
}