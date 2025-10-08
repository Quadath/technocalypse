using UnityEngine;

public class World: IWorld
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Depth { get; private set; }

    public int SizeInChunksX { get; private set; }
    public int SizeInChunksY { get; private set; }
    public int SizeInChunksZ { get; private set; }

    public int ChunkSizeX { get; private set; } = 16;
    public int ChunkSizeY { get; private set; } = 16;
    public int ChunkSizeZ { get; private set; } = 16;
    public bool isGenerationDone { get; private set; } = false;

    Chunk[,,] chunks;

    public World(int width, int height, int depth) {
        Width = width;
        Height = height;
        Depth = depth;

        SizeInChunksX = Mathf.FloorToInt(width / ChunkSizeX);
        SizeInChunksY = Mathf.FloorToInt(height / ChunkSizeY);
        SizeInChunksZ = Mathf.FloorToInt(depth / ChunkSizeZ);

        chunks = new Chunk[SizeInChunksX, SizeInChunksY, SizeInChunksZ];
        for (int x = 0; x < SizeInChunksX; x++)
        {
            for (int z = 0; z < SizeInChunksZ; z++)
            {
                for (int y = 0; y < SizeInChunksY; y++)
                {
                    chunks[x, y, z] = new Chunk(x, y, z, ChunkSizeX, ChunkSizeY, ChunkSizeZ, this);
                }
            }
        }
    }

    public Chunk GetChunk(int x, int y, int z) {
        if (x < 0 || x >= Width || y < 0 || y >= Height || z < 0 || z >= Depth)
            return null;
        return chunks[x, y, z];
    }

    public IBlock GetBlock(int x, int y, int z)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height || z < 0 || z >= Depth)
            return null;
        int cx = x % ChunkSizeX;
        int cy = y % ChunkSizeY;
        int cz = z % ChunkSizeZ;
        int chunk_x = (x - cx) / ChunkSizeX;
        int chunk_y = (y - cy) / ChunkSizeY;
        int chunk_z = (z - cz) / ChunkSizeZ;

        return chunks[chunk_x, chunk_y, chunk_z].GetBlock(cx, cy, cz);
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height || z < 0 || z >= Depth)
            return;
        int cx = x % ChunkSizeX;
        int cy = y % ChunkSizeY;
        int cz = z % ChunkSizeZ;
        int chunk_x = (x - cx) / ChunkSizeX;
        int chunk_y = (y - cy) / ChunkSizeY;
        int chunk_z = (z - cz) / ChunkSizeZ;

        chunks[chunk_x, chunk_y, chunk_z].SetBlock(cx, cy, cz, block);
    }

    public void onGenerationEnded() {
        for(int x = 0; x < SizeInChunksX; x++) {
            for(int z = 0; z < SizeInChunksZ; z++) {
                for(int y = 0; y < SizeInChunksY; y++) {
                    chunks[x, y, z].UpdateMesh();
                }
            }
        }
    }
}