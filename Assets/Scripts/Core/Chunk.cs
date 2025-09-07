using UnityEngine;

public class Chunk
{
    private IChunkRenderer renderer;
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Depth { get; private set; }
    private World world;
    private Block[,,] blocks;

    public Chunk(int x, int y, int z, int width, int height, int depth, World w) {
        X = x;
        Y = y;
        Z = z;
        Width = width;
        Height = height;
        Depth = depth;
        blocks = new Block[Width, Height, Depth];
        world = w;
    }

    public void SetBlock(int x, int y, int z , Block block) {
        if (x < 0 || x >= Width || y < 0 || y >= Height || z < 0 || z >= Depth)
            return;
        blocks[x, y, z] = block;
    }

    public Block GetBlock(int x, int y, int z)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height || z < 0 || z >= Depth)
            return null;
        return blocks[x, y, z];
    }

    public void UpdateMesh()
    {
        // Debug.Log("Updating chunk " + X + " " + Y + " " + Z);
        renderer.Render(world, new Vector3Int(X, Y, Z));
    }

    public void SetRenderer(IChunkRenderer r) {
        renderer = r;
    }
}
