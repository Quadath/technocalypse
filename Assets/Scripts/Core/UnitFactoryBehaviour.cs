using UnityEngine;

public class UnitFactoryBehaviour : IBuildingBehaviour
{
    Building building;
    IUnitData unitData;

    public UnitFactoryBehaviour(Building building)
    {
        this.building = building;
    }

    public void OnTick(float deltaTime)
    {
        
    }
}
