using UnityEngine;

public class UnitFactoryBehaviour : IBuildingBehaviour
{
    private Building building;
    private IUnitData unitData;
    

    public UnitFactoryBehaviour(Building building)
    {
        this.building = building;
    }

    public void OnTick(float deltaTime)
    {
        
    }
}
