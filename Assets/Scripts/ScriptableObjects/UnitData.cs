using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "UnitData", menuName = "Basics/UnitData")]
public class UnitData : ScriptableObject, IUnitData
{
	[SerializeField] private string unitName;
    public string UnitName => unitName;
    [SerializeField] private GameObject prefab;
	public GameObject Prefab => prefab;
	[SerializeField] private float speed;
	public float Speed => speed;
	[SerializeField] private int hitPoints;
	public int HitPoints => hitPoints;
	[SerializeField] private List<ScriptableObject> behaviours; 

  	public List<IUnitBehaviourData> Behaviours =>
    	behaviours.OfType<IUnitBehaviourData>().ToList();
}
