public class Block: IBlock
{
    public BlockType Type { get; private set; }
    public bool IsSlope { get; private set; }
    public int TraversalCost { get; private set; } // для pathfinding
    
    public Block(BlockType type, bool isSlope = false, int traversalCost = 1)
    {
        Type = type;
        IsSlope = isSlope;
        TraversalCost = traversalCost;
    }
}