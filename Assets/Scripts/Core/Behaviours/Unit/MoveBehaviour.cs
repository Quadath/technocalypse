using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : IUnitBehaviour
{
    private readonly IUnit _unit;
    private readonly Queue<Vector3> _path = new();

    public MoveBehaviour(IUnit unit)
    {
        _unit = unit;
    }

    public void SetPath(List<Vector3Int> newPath)
    {
        _path.Clear();
        if (newPath == null) return;
        foreach (var p in newPath)
            _path.Enqueue(new Vector3(p.x + 0.5f, p.y, p.z + 0.5f));
    }

    public void OnTick(float deltaTime)
    {
        if (_path.Count == 0) 
        {
            _unit.TargetDirection = Vector3.zero;
            return;
        }

        var target = _path.Peek();
        _unit.NextPathPointPosition = target;
        var dir = target - _unit.Transform.position;
        dir.y = 0f; // 2D plane movement
        _unit.TargetDirection = dir.normalized;

        if (!(dir.sqrMagnitude < 0.1f)) return; 
        _path.Dequeue(); // close enough

        //var move = dir.normalized * _unit.Speed * deltaTime;
        // якщо хочеш не пропустити ціль при великих швидкостях:
        // if (move.sqrMagnitude >= dir.sqrMagnitude) _unit.Position = target;
        // else _unit.Position += move;

        // _unit.Direction = dir.normalized;
    }
}