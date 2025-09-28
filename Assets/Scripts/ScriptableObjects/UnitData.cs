using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public string name;
	public GameObject prefab;
	public float speed;
	public int hitPoints;
}
