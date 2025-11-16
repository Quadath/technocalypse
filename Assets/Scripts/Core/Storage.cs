using System.Collections.Generic;

public class Storage: IStorage
{
    private Dictionary<ResourceTypeID, StorageCell> _cells;

    public Storage(List<(ResourceTypeID id, int capacity)> setup)
    {
        _cells = new Dictionary<ResourceTypeID, StorageCell>();

        foreach (var entry in setup)
            _cells[entry.id] = new StorageCell(entry.id, entry.capacity);
    }

    public bool Add(ResourceTypeID id, int amount)
    {
        return _cells[id].Add(amount);
    }

    public bool TrySpend(ResourceTypeID resourceTypeID, int amount)
    {
        return _cells[resourceTypeID].TrySpend(amount);
    }
}
