using Game.Shared.SO;
using Game.Shared.Systems;
using UnityEngine;

public class UnitFactoryBehaviour : IBuildingBehaviour
{
    private readonly IBuilding _building;
    private readonly IUnitData _unitData;
    private readonly ResourceInstance _requiredResourceInstance;
    private readonly IUnitSpawner _spawner;
    private readonly IResourceManager _resourceManager;
    private readonly float _productionTime;
    
    private bool _resourcesWithdrawn;
    private float _timer;
    
    private int ProductionCost => _requiredResourceInstance.amount;


    public UnitFactoryBehaviour(IBuilding building, IUnitData unitData, ResourceInstance requiredResourceInstance,
        float productionTime, IResourceManager resourceManager, IUnitSpawner spawner)
    {
        _building = building;
        _unitData = unitData;
        _requiredResourceInstance = requiredResourceInstance;
        _productionTime = productionTime;
        _resourceManager = resourceManager;
        _spawner = spawner;
    }

    public void OnTick(float deltaTime)
    {
        //Оскільки ворог немає системи ресурсів, спавнимо безкоштовно
		if (_building.Player != 0) _resourcesWithdrawn = true;
        if (!_resourcesWithdrawn)
        {
            _resourcesWithdrawn = _resourceManager.TrySpend(_requiredResourceInstance.resourceTypeID, ProductionCost);
            if (!_resourcesWithdrawn) return;
        }
		_timer += deltaTime;
        if (_timer < _productionTime)
            return;
        _spawner.SpawnAt(_building.Origin + new Vector3(1, 0.5f, 2.5f), Quaternion.identity, _unitData, _building.Player);
		_timer = 0;
		_resourcesWithdrawn = false;
    }
}
