using UnityEngine;

public interface ITargetable
{
    Transform Transform { get; }
    void TakeDamage(int amount);
}
