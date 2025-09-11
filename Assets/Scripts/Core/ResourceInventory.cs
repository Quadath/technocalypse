using System.Collections.Generic;

public class ResourceInventory
{
    private Dictionary<string, Resource> resources = new();

    public void AddResource(string name, int amount)
    {
        if (!resources.ContainsKey(name))
            resources[name] = new Resource(name, 0);

        resources[name].Add(amount);
    }

    public bool TrySpend(string name, int amount)
    {
        if (!resources.ContainsKey(name)) return false;
        return resources[name].TrySpend(amount);
    }

    public int GetAmount(string name)
    {
        return resources.ContainsKey(name) ? resources[name].Amount : 0;
    }

    public IReadOnlyDictionary<string, Resource> GetAll() => resources;
}