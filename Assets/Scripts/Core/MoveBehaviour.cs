using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : IUnitBehaviour
{
    private readonly Unit unit;
    private readonly Queue<Vector3> path = new();

    public MoveBehaviour(Unit unit)
    {
        this.unit = unit;
    }

    public void SetPath(List<Vector3Int> newPath)
    {
        path.Clear();
        if (newPath == null) return;
        foreach (var p in newPath)
            path.Enqueue(new Vector3(p.x + 0.5f, p.y, p.z + 0.5f));
    }

    public void OnTick(float deltaTime)
    {
        if (path.Count == 0) 
        {
            unit.TargetDirection = Vector3.zero;
            return;
        }

        Vector3 target = path.Peek();
        unit.NextPathPointPosition = target;
        Vector3 dir = target - unit.Transform.position;
        dir.y = 0f; // 2D plane movement
        unit.TargetDirection = dir.normalized;

        if (dir.sqrMagnitude < 0.1f) // close enough
        {
            path.Dequeue();
            return;
        }

        Vector3 move = dir.normalized * unit.Speed * deltaTime;
        // якщо хочеш не пропустити ціль при великих швидкостях:
        // if (move.sqrMagnitude >= dir.sqrMagnitude) unit.Position = target;
        // else unit.Position += move;

        // unit.Direction = dir.normalized;
    }
}