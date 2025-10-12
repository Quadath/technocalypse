using System;
using UnityEngine;
using System.Collections.Generic;

public class Unit : ITargetable, IUnit
{
    public string Name { get; private set; }
    public int Player { get; private set; }
    public float Speed { get; private set; }
    public int HitPoints { get; private set; }
    public int MaxHitPoints { get; }
	public int DetectionRange { get; private set; } = 20;
    public Transform Transform { get; private set; }    // посилання на GameObject
    public Rigidbody Rigidbody { get; private set; }    // для фізики
    public Vector3 NextPathPointPosition { get; set; }
    public Vector3 TargetDirection { get; set; }
    public Vector3 GoalPosition { get; set; }
    public bool HasGoal => GoalPosition != Vector3.zero;
    public PathfindingGrid PathfindingGrid;
    private readonly List<IUnitBehaviour> behaviours = new();
    public bool IsAlive => HitPoints > 0;
	public string Message { get; private set; }
	public event Action<string, string> OnMessage;
    private event Action<Unit> OnDeath;
    
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
    public void MoveTo(Vector3Int g)
    {
        var mb = GetBehaviour<MoveBehaviour>();
        var start = Vector3Int.RoundToInt(Transform.position);
        var finder = new AStar3D();
        var gridPath = finder.FindPath(start, g, pos => PathfindingGrid.IsWalkable(pos));
        if (mb != null) mb.SetPath(gridPath);
    }

    public void AddBehaviour(IUnitBehaviour b) => behaviours.Add(b);
    public void AddOnDeathListener(Action<Unit> listener) => OnDeath += listener;
    public void RemoveOnDeathListener(Action<Unit> listener) => OnDeath -= listener;
    
    public T GetBehaviour<T>() where T : class, IUnitBehaviour
    {
        foreach (var b in behaviours)
            if (b is T t) return t;
        return null;
    }

    public void TakeDamage(int amount)
    {
        HitPoints -= amount;
        if (HitPoints <= 0)
        {
            Die();
            return;
        } 
		DebugMessage("Unit", $"got damage. HP is {HitPoints}");
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
    }

	public void DebugMessage(string source, string msg) {
		Message = msg;
		OnMessage?.Invoke(source, Message);
	}
}