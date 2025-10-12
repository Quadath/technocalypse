using UnityEngine;
using System.Collections.Generic;
public interface IUnitData
{
    public string UnitName { get; }
    public GameObject Prefab { get; }
    public float Speed { get; }
    public int HitPoints { get; }
    public List<IUnitBehaviourData> Behaviours { get; }
}
