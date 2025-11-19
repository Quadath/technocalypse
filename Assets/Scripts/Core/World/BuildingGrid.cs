using UnityEngine;

public class BuildingGrid
{
    private readonly World _world;
    private readonly Building[,,] _grid;
    private readonly int _width, _height, _depth;

    public BuildingGrid(int width, int height, int depth, World world)
    {
        _width = width;
        _height = height;
        _depth = depth;
        _world = world;
        _grid = new Building[width, height, depth];
    }

    public bool CanPlaceBuilding(Building b, int x, int y, int z)
    {
        if (x < 0 || y < 0 || z < 0 || x + b.Size.x > _width || y + b.Size.y > _height || z + b.Size.z > _depth)
            return false;
        for (var dx = 0; dx < b.Size.x; dx++)
            for (var dy = 0; dy < b.Size.y; dy++)
                for (var dz = 0; dz < b.Size.z; dz++)
                {
                    if (_grid[x + dx, y + dy, z + dz] != null)
                        return false; // клітина зайнята
                    if (_world.GetBlock(x + dx, y + dy, z + dz).Type != BlockType.Air)
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
                    _grid[x + dx, y + dy, z + dz] = b;
                }

        // Можна відразу встановити позицію у сцені
        if (b.GameObject)
        {
            b.GameObject.transform.position = new Vector3(x, y, z);
        }

        return true;
    }

    // Видаляємо будівлю з сітки
    public void RemoveBuilding(Building b)
    {
        for (int x0 = 0; x0 < _width; x0++)
            for (int y0 = 0; y0 < _height; y0++)
                for (int z0 = 0; z0 < _depth; z0++)
                {
                    if (_grid[x0, y0, z0] == b)
                        _grid[x0, y0, z0] = null;
                }

        // Можна одразу знищити об'єкт у сцені
        // if (b.GameObject != null)
        //     GameObject.Destroy(b.GameObject);
    }

    // Перевірити, чи клітина зайнята
    public bool IsCellOccupied(int x, int y, int z)
    {
        return _grid[x, y, z] != null;
    }

    // Отримати будівлю за координатами
    public Building GetBuildingAt(int x, int y, int z)
    {
        return _grid[x, y, z];
    }
}
