using System.Collections.Generic;
using UnityEngine;

public class StorageBehaviour : IBuildingBehaviour
{
    private readonly Storage _storage;

    public StorageBehaviour(List<StorageCellDataPair> storageSetup, int capacity, bool strict)
    {
        _storage = new Storage(storageSetup, capacity, strict);
    }

    public bool TrySpend(ResourceTypeID resourceTypeID, int amount) =>
        _storage.TrySpend(resourceTypeID, amount);

    public bool InsertResource(ResourceTypeID resourceTypeID, int amount) =>
        _storage.TrySpend(resourceTypeID, amount);
    public void OnTick(float deltaTime)
    {
        
    }
}
