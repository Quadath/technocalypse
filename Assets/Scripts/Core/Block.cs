public class Block
{
    public BlockType Type { get; private set; }
    public bool IsSlope { get; private set; }
    public float TraversalCost { get; private set; } // для pathfinding

    public enum BlockType
    {
        Air,
        Sand,
        SandStone
    }

    public Block(BlockType type, bool isSlope = false, float traversalCost = 1f)
    {
        Type = type;
        IsSlope = isSlope;
        TraversalCost = traversalCost;
    }
}