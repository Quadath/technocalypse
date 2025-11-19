using UnityEngine;

public interface IUnit
{
    Transform Transform { get; }   
    public Vector3 NextPathPointPosition { get; set; } 
    public Vector3 TargetDirection { get; set; }
}
