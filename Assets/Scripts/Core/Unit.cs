using UnityEngine;

public class Unit : ITargetable
{
    public string Name { get; }
    public Transform Transform { get; }    // посилання на GameObject
    public Rigidbody Rigidbody { get; }    // для фізики
    public int Player;
    public float Speed { get; }
    public int HitPoints;
    public int MaxHitPoints;
    public bool IsAlive => HitPoints > 0;
    public float AttackRange;
    public Unit Target = null;
    public Vector3 GoalPosition;
    public bool HasGoal => GoalPosition != Vector3.zero;
    public Unit(string name, Transform transform, Rigidbody rigidbody, float speed)
    {
        Name = name;
        Transform = transform;
        Rigidbody = rigidbody;
        Speed = speed;
    }

    public void TakeDamage(int amount)
    {
        HitPoints -= amount;
    }
}