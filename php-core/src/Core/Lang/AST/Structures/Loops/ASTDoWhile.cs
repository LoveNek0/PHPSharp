using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Loops;

public class ASTDoWhile : ASTBlock
{
    public ASTNode Condition => _condition;
    
    internal ASTNode _condition;
    
    internal ASTDoWhile(TokenItem token) : base(token)
    {
    }
}