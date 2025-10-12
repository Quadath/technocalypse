using UnityEngine;

public interface IUnit
{
    string Name { get; }
    int Player { get; }
    float Speed { get; }
    int HitPoints { get; }
    int MaxHitPoints { get; }
	int DetectionRange { get; }
    Transform Transform { get; }   
    Rigidbody Rigidbody { get; }   
    public Vector3 NextPathPointPosition { get; set; } 
    public Vector3 TargetDirection { get; set; }
    public Vector3 GoalPosition { get; set; }

    
    
    
    
    
    public void DebugMessage(string source, string msg);
}
