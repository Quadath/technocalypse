using UnityEngine;

public class WorldGenerator
{
    public float noiseScale = 0.004f;

    public void Generate(World world, int width, int height, int depth)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                // Генеруємо висоту блоку за перлін-шумом
                float sample = Mathf.PerlinNoise(x * noiseScale, z * noiseScale);
                int yMax = Mathf.FloorToInt(sample * height);

                for (int y = 0; y <= yMax; y++)
                {
                    Block.BlockType type;

                    // Наприклад, низ — пісок, верх — камінь
                    if (y < yMax * 0.3f) type = Block.BlockType.Sand;
                    else type = Block.BlockType.SandStone;

                    world.SetBlock(x, y, z, new Block(type));
                }

                // Заповнюємо решту повітрям
                for (int y = yMax + 1; y < height; y++)
                {
                    world.SetBlock(x, y, z, new Block(Block.BlockType.Air));
                }
            }
        }

        world.onGenerationEnded();
    }
}