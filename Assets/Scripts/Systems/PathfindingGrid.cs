using UnityEngine;

public class PathfindingGrid : MonoBehaviour
{
    public WorldManager manager;
    private World world;
    private BuildingGrid buildingGrid;

    void Start()
    {
        world = manager.world;
        buildingGrid = manager.BuildingGrid;
    }

    public bool IsWalkable(Vector3Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.z < 0 ||
            pos.x >= world.Width ||
            pos.y >= world.Height ||
            pos.z >= world.Depth)
            return false;

        var block = world.GetBlock(pos.x, pos.y, pos.z);
        if (block.Type == Block.BlockType.Air)
        {
            if (pos.y > 0)
            {
                var below = world.GetBlock(pos.x, pos.y - 1, pos.z);
                if (below.Type != Block.BlockType.Air)
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
