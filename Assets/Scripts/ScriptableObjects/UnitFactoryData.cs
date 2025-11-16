using UnityEngine;

[CreateAssetMenu(fileName = "UnitFactoryData", menuName = "Behaviour/Building/UnitFactoryData")]
public class UnitFactoryData : BuildingBehaviourData
{
    public UnitData producedUnitData;
    public ResourceData requiredResource;
    public int requiredResourceCount;
    
    
    
    public override IBuildingBehaviour CreateBehaviour(IBuilding owner, IServiceProvider services)
    {
        var resourceManager = services.Get<IResourceManager>();
        var unitSpawner = services.Get<IUnitSpawner>();
        return new UnitFactoryBehaviour(owner, producedUnitData, new ResourceInstance(requiredResource.resourceTypeID, requiredResourceCount),
            4f, resourceManager, unitSpawner);
    }
}
