using UnityEngine;

public class UnitFactoryBehaviour : IBuildingBehaviour
{
    private IBuilding building;
    private IUnitData unitData;
    private Resource reqiuredResource;
    private int productionCost => reqiuredResource.Amount;
    private float productionTime;
    private float timer;
    private bool resourcesWithdrawed = false;
    private IResourceManager resourceManager;
    private IUnitSpawner spawner;


    public UnitFactoryBehaviour(IBuilding building, IUnitData unitData, Resource reqiuredResource,
        float productionTime, IResourceManager resourceManager, IUnitSpawner spawner)
    {
        this.building = building;
        this.unitData = unitData;
        this.reqiuredResource = reqiuredResource;
        this.productionTime = productionTime;
        this.resourceManager = resourceManager;
        this.spawner = spawner;
    }

    public void OnTick(float deltaTime)
    {
		if (building.Player != 0) resourcesWithdrawed = true;
        if (!resourcesWithdrawed)
        {
            resourcesWithdrawed = resourceManager.TrySpend(reqiuredResource.Name, productionCost);
            return;
        }
		timer += deltaTime;
        if (timer < productionTime) 
            return;
        spawner.SpawnAt(building.Origin + new Vector3(1, 0.5f, 2.5f), Quaternion.identity, unitData, building.Player);
		timer = 0;
		resourcesWithdrawed = false;
    }
}
