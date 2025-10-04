using UnityEngine;
using System.Collections.Generic;

public class Unit : ITargetable
{
    public string Name { get; }
    public int Player;
    public float Speed { get; }
    public int HitPoints;
    public int MaxHitPoints;
	public int DetectionRange = 20;
    public Transform Transform { get; }    // посилання на GameObject
    public Rigidbody Rigidbody { get; }    // для фізики
    public Vector3 NextPathPointPosition;
    public Vector3 TargetDirection;
    public Vector3 GoalPosition;
    public bool HasGoal => GoalPosition != Vector3.zero;
    public PathfindingGrid PathfindingGrid;
    private readonly List<IUnitBehaviour> behaviours = new();
    public bool IsAlive => HitPoints > 0;
	public string Message { get; private set; }
	public event System.Action<string> OnMessage;
    
    public Unit(string name, int player, Transform transform, Rigidbody rigidbody, float speed, int hp)
    {
        Name = name;
        Player = player;
        Transform = transform;
        Rigidbody = rigidbody;
        Speed = speed;
		HitPoints = hp;
		MaxHitPoints = hp;
    }

    public void Tick(float deltaTime)
    {
        foreach (var b in behaviours)
            b.OnTick(deltaTime);
    }
    // public void SetMovePath(IEnumerable<Vector3> worldPath)
    // {
    //     var mb = GetBehaviour<MoveBehaviour>();
    //     if (mb != null) mb.SetPath(worldPath);
    // }
    public void MoveTo(Vector3Int g)
    {
        var mb = GetBehaviour<MoveBehaviour>();
        var start = Vector3Int.RoundToInt(Transform.position);
        var finder = new AStar3D();
        var gridPath = finder.FindPath(start, g, pos => PathfindingGrid.IsWalkable(pos));
        if (mb != null) mb.SetPath(gridPath);
    }

    public void AddBehaviour(IUnitBehaviour b) => behaviours.Add(b);
    
    public T GetBehaviour<T>() where T : class, IUnitBehaviour
    {
        foreach (var b in behaviours)
            if (b is T t) return t;
        return null;
    }

    public void TakeDamage(int amount)
    {
        HitPoints -= amount;
		DebugMessage($"<color=cyan>[{"Unit"}]</color>: got damage. HP is {HitPoints}");
    }

	public void DebugMessage(string msg) {
		Message = msg;
		OnMessage?.Invoke(Message);
	}
}