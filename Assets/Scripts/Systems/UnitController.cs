using UnityEngine;
public class UnitController
{
    private Unit unit;
    private Vector3? target;
    public UnitView unitView;

    public UnitController(Unit unit)
    {
        this.unit = unit;
    }

    public PathfindingGrid grid;

    public void MoveTo(Vector3Int goal)
    {
        var start = Vector3Int.RoundToInt(unitView.transform.position);
        var finder = new AStar3D();
        var gridPath = finder.FindPath(start, goal, pos => grid.IsWalkable(pos));

        unitView.SetPath(gridPath, grid);
    }
}