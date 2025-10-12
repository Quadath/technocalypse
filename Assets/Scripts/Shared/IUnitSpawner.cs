using UnityEngine;

public interface IUnitSpawner
{
    public void SpawnAt(Vector3 worldPos, Quaternion rot, IUnitData unitData, int player);
}
