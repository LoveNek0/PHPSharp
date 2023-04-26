using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Function;

public class ASTIf : ASTNode
{
    public ASTNode Expression => _expression;
    public ASTNode[] IfBlock => _ifBlock.ToArray();
    public ASTNode[] ElseBlock => _elseBlock.ToArray();
    
    
    internal ASTNode _expression;
    internal List<ASTNode> _ifBlock = new List<ASTNode>();
    internal List<ASTNode> _elseBlock = new List<ASTNode>();

    internal ASTIf(TokenItem token) : base(token)
    {
    }

    public override string ToString() => $"if({Expression}){{\n{String.Join("\n", _ifBlock)}\n}}" + (_elseBlock.Count > 0 ? $"else{{\n{String.Join("\n", _elseBlock)}\n}}" : "");
}