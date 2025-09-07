using UnityEngine;
public class UnitController
{
    private Unit unit;
    private Vector3? goal;
    public UnitView unitView;
    public PathfindingGrid grid;


    public UnitController(Unit unit)
    {
        this.unit = unit;
    }


    public void MoveTo(Vector3Int g)
    {
        var start = Vector3Int.RoundToInt(unitView.transform.position);
        var finder = new AStar3D();
        var gridPath = finder.FindPath(start, g, pos => grid.IsWalkable(pos));

        unitView.SetPath(gridPath, grid);
    }
}