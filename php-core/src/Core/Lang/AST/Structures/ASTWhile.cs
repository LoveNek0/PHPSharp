using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures;

public class ASTWhile : ASTNode
{
    public ASTNode Expression => _expression;
    public ASTNode[] Lines => _lines.ToArray();
    
    internal ASTNode _expression;
    internal List<ASTNode> _lines = new List<ASTNode>();
    
    internal ASTWhile(TokenItem token) : base(token)
    {
    }
}