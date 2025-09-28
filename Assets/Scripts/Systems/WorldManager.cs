using UnityEngine;

public class WorldManager : MonoBehaviour
{

    public int worldWidth = 256;
    public int worldHeight = 64;
    public int worldDepth = 256;
    public World World { get; private set; }
    public BuildingGrid BuildingGrid { get; private set; }
    public PathfindingGrid PathfindingGrid { get; private set; }

    public Material mat;

    void Awake()
    {
        World = new World(worldWidth, worldHeight, worldDepth);
        for (int x = 0; x < World.SizeInChunksX; x++)
        {
            for (int z = 0; z < World.SizeInChunksZ; z++)
            {
                for (int y = 0; y < World.SizeInChunksY; y++)
                {
                    // Створюємо пустий об’єкт у сцені
                    GameObject chunkGO = new GameObject($"Chunk_{x}_{y}_{z}");
                    chunkGO.transform.parent = this.transform; // щоб все лежало під WorldManager
                    chunkGO.transform.position = new Vector3(x * World.ChunkSizeX,
                        y * World.ChunkSizeY,
                        z * World.ChunkSizeZ);

                    // Додаємо рендерер
                    chunkGO.AddComponent<MeshFilter>();
                    chunkGO.layer = 6;
                    MeshRenderer r = chunkGO.AddComponent<MeshRenderer>();
                    chunkGO.AddComponent<MeshCollider>();
                    ChunkRenderer renderer = chunkGO.AddComponent<ChunkRenderer>();
                    r.material = mat;

                    // Беремо відповідний chunk з world
                    Chunk chunk = World.GetChunk(x, y, z);

                    // Зв’язуємо
                    chunk.SetRenderer(renderer);
                }
            }
        }
        BuildingGrid = new BuildingGrid(worldWidth, worldHeight, worldDepth, World);
        PathfindingGrid = new PathfindingGrid(World, BuildingGrid);
    }

    void Start()
    {
        WorldGenerator generator = new WorldGenerator();
        generator.Generate(World, worldWidth, worldHeight, worldDepth);
    }
}