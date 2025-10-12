using UnityEngine;

public interface IBuildingSpawner
{
    public void SpawnAt(Vector3Int placePos, Quaternion rot, IBuildingData buildingData, int player);
}
