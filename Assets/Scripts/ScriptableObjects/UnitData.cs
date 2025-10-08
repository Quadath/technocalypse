using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "UnitData", menuName = "Basics/UnitData")]
public class UnitData : ScriptableObject, IUnitData
{
    public string UnitName;
	public GameObject Prefab;
	public float Speed;
	public int HitPoints;
	[SerializeField] private List<ScriptableObject> behaviours; 

  	public IEnumerable<IUnitBehaviourData> Behaviours =>
    	behaviours.OfType<IUnitBehaviourData>();
}
