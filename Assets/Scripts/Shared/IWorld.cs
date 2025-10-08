public interface IWorld
{
    int Width { get; } 
    int Height { get; } 
    int Depth { get; } 

    int ChunkSizeX { get; }
    int ChunkSizeY { get; }
    int ChunkSizeZ { get; }

	public IBlock GetBlock(int x, int y, int z);
}
