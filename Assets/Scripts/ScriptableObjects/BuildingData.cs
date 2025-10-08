using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Basics/Building")]
public class BuildingData : ScriptableObject, IBuildingData
{
    [SerializeField] private string name;
    public string Name => name;
    [SerializeField] private Vector3Int size;
    public Vector3Int Size => size;
    [SerializeField] private int hitPoints;
    public int HitPoints => hitPoints;
    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;

    [SerializeField] private List<BuildingBehaviourData> behaviours;
    private IReadOnlyList<IBuildingBehaviourData> _behavioursView;
    public IReadOnlyList<IBuildingBehaviourData> Behaviours
        => _behavioursView ??= behaviours.ConvertAll<IBuildingBehaviourData>(b => b);
}
