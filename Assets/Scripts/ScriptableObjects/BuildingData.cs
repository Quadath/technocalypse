using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Buildings/Building")]
public class BuildingData : ScriptableObject
{
    public string Name;
    public Vector3Int Size;
    public int HitPoints;
    public GameObject prefab;
    
    public List<BuildingBehaviourData> behaviours;
}
