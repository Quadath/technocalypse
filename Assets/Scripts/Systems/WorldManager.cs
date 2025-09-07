using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public World world { get; private set; }

    public int worldWidth = 256;
    public int worldHeight = 64;
    public int worldDepth = 256;

    public Material mat;

    void Awake() {
        world = new World(worldWidth, worldHeight, worldDepth);
        for (int x = 0; x < world.SizeInChunksX; x++)
        {
            for (int z = 0; z < world.SizeInChunksZ; z++)
            {
                for (int y = 0; y < world.SizeInChunksY; y++)
                {
                    // Створюємо пустий об’єкт у сцені
                    GameObject chunkGO = new GameObject($"Chunk_{x}_{y}_{z}");
                    chunkGO.transform.parent = this.transform; // щоб все лежало під WorldManager
                    chunkGO.transform.position = new Vector3(x * world.ChunkSizeX, 
                        y * world.ChunkSizeY, 
                        z * world.ChunkSizeZ);

                    // Додаємо рендерер
                    // chunkGO.material = mat;
                    chunkGO.AddComponent<MeshFilter>();
                    chunkGO.layer = 6;
                    MeshRenderer r = chunkGO.AddComponent<MeshRenderer>();
                    chunkGO.AddComponent<MeshCollider>();
                    ChunkRenderer renderer = chunkGO.AddComponent<ChunkRenderer>();
                    r.material = mat;

                    // Беремо відповідний chunk з world
                    Chunk chunk = world.GetChunk(x, y, z);

                    // Зв’язуємо
                    chunk.SetRenderer(renderer);
                }
            }
        }
    }

    void Start()
    {
        WorldGenerator generator = new WorldGenerator();
        generator.Generate(world, worldWidth, worldHeight, worldDepth);
    }
}