using UnityEngine;

public interface ISpawnData
{
    Vector3 Position { get; }
    Quaternion Rotation { get; }
}


[System.Serializable]
public class UnitSpawnData: ISpawnData
{
    public UnitData unitData;
    public int player;
    public Vector3 position;
    public Quaternion rotation;

    public Vector3 Position => position;
    public Quaternion Rotation => rotation;
}
[System.Serializable]
public class BuildingSpawnData
{
    public BuildingData buildingData;
    public int player;
    public Vector3Int Position;
    public Quaternion Rotation;
}

