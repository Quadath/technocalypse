using UnityEngine;

public class PathfindingGrid
{
    private World world;
    private BuildingGrid buildingGrid;

    public PathfindingGrid(World world, BuildingGrid buidlingGrid)
    {
        this.world = world;
        this.buildingGrid = buidlingGrid;
    }
    public bool IsWalkable(Vector3Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.z < 0 ||
            pos.x >= world.Width ||
            pos.y >= world.Height ||
            pos.z >= world.Depth)
            return false;

        var block = world.GetBlock(pos.x, pos.y, pos.z);
        if (block.Type == BlockType.Air)
        {
            if (pos.y > 0)
            {
                var below = world.GetBlock(pos.x, pos.y - 1, pos.z);
                if (below.Type != BlockType.Air)
                {
                    if (!buildingGrid.IsCellOccupied(pos.x, pos.y, pos.z))
                        return true;
                }
            }
            return false;
        }
        

        return false;
    }
}
