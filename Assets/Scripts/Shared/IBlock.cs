public interface IBlock
{
    public BlockType Type { get; }
}

public enum BlockType
{
    Air,
    Sand,
    SandStone
}
