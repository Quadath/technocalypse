using UnityEngine;

public class Unit
{
    public string Name { get; }
    public Transform Transform { get; }    // посилання на GameObject
    public Rigidbody Rigidbody { get; }    // для фізики
    public float Speed { get; }
    public Vector3 TargetPosition;
    public bool HasTarget => TargetPosition != Vector3.zero;
    public Unit(string name, Transform transform, Rigidbody rigidbody, float speed)
    {
        Name = name;
        Transform = transform;
        Rigidbody = rigidbody;
        Speed = speed;
    }
}