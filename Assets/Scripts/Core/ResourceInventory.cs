using System.Collections.Generic;

public class ResourceInventory
{
    private Dictionary<ResourceTypeID, IResourceInstance> resources = new();

    public void AddResource(ResourceTypeID id, int amount)
    {
        if (!resources.ContainsKey(id))
            resources[id] = new ResourceInstance(id, 0);

        resources[id].Add(amount);
    }

    public bool TrySpend(ResourceTypeID id, int amount)
    {
        if (!resources.ContainsKey(id)) return false;
        return resources[id].TrySpend(amount);
    }

    public int GetAmount(ResourceTypeID id)
    {
        return resources.ContainsKey(id) ? resources[id].amount : 0;
    }

    public IReadOnlyDictionary<ResourceTypeID, IResourceInstance> GetAll() => resources;
}