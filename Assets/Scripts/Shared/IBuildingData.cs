using UnityEngine;
using System.Collections.Generic;

public interface IBuildingData
{
    string Name { get; } 
	GameObject Prefab { get; }
    int HitPoints { get; }
	Vector3Int Size { get; }
    IReadOnlyList<IBuildingBehaviourData> Behaviours { get; }
}
