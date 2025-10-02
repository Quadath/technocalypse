using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public string UnitName;
	public GameObject Prefab;
	public float Speed;
	public int HitPoints;
	[SerializeField] private List<ScriptableObject> behaviours; 

  	public IEnumerable<IUnitBehaviourData> Behaviours =>
    	behaviours.OfType<IUnitBehaviourData>();
}
