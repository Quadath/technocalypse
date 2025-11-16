using System.Collections.Generic;
using UnityEngine;

public class StorageBehaviour : IBuildingBehaviour
{
    private Storage _storage;

    public StorageBehaviour(List<(ResourceTypeID id, int capacity)> storageSetup)
    {
        _storage = new Storage(storageSetup);
    }

    public bool TrySpend(ResourceTypeID resourceTypeID, int amount)
    {
        return _storage.TrySpend(resourceTypeID, amount);
    }

    public bool InsertResource(ResourceTypeID resourceTypeID, int amount)
    {
        return _storage.TrySpend(resourceTypeID, amount);
    }
    public void OnTick(float deltaTime)
    {
        
    }
}
