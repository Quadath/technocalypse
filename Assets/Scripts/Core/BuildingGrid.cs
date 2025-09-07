using UnityEngine;

public class BuildingGrid
{
    private World world;
    private Building[,,] grid;
    private int width, height, depth;

    public BuildingGrid(int width, int height, int depth, World world)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.world = world;
        grid = new Building[width, height, depth];
    }

    public bool CanPlaceBuilding(Building b, int x, int y, int z)
    {
        if (x < 0 || y < 0 || z < 0 || x + b.Size.x > width || y + b.Size.y > height || z + b.Size.z > depth)
            return false;
        for (int dx = 0; dx < b.Size.x; dx++)
            for (int dy = 0; dy < b.Size.y; dy++)
                for (int dz = 0; dz < b.Size.z; dz++)
                {
                    if (grid[x + dx, y + dy, z + dz] != null)
                        return false; // клітина зайнята
                    if (world.GetBlock(x + dx, y + dy, z + dz).Type != Block.BlockType.Air)
                        return false;
                }
        return true;
    }

    public bool PlaceBuilding(Building b, int x, int y, int z)
    {
        if (!CanPlaceBuilding(b, x, y, z))
            return false;

        for (int dx = 0; dx < b.Size.x; dx++)
            for (int dy = 0; dy < b.Size.y; dy++)
                for (int dz = 0; dz < b.Size.z; dz++)
                {
                    grid[x + dx, y + dy, z + dz] = b;
                }

        // Можна відразу встановити позицію у сцені
        if (b.GameObject != null)
        {
            b.GameObject.transform.position = new Vector3(x, y, z);
        }

        return true;
    }

    // Видаляємо будівлю з сітки
    public void RemoveBuilding(Building b)
    {
        for (int x0 = 0; x0 < width; x0++)
            for (int y0 = 0; y0 < height; y0++)
                for (int z0 = 0; z0 < depth; z0++)
                {
                    if (grid[x0, y0, z0] == b)
                        grid[x0, y0, z0] = null;
                }

        // Можна одразу знищити об'єкт у сцені
        // if (b.GameObject != null)
        //     GameObject.Destroy(b.GameObject);
    }

    // Перевірити, чи клітина зайнята
    public bool IsCellOccupied(int x, int y, int z)
    {
        return grid[x, y, z] != null;
    }

    // Отримати будівлю за координатами
    public Building GetBuildingAt(int x, int y, int z)
    {
        return grid[x, y, z];
    }
}
