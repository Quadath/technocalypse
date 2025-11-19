using System.Collections.Generic;
using System.Linq;

public class Storage: IStorage
{
    private Dictionary<ResourceTypeID, StorageCell> _cells;
    private bool _strict;
    private int _capacity;

    public Storage(List<StorageCellDataPair> setup, int capacity = 0, bool strict = false)
    {
        //Перетворюємо List на Dictionary для продуктивості
        _cells = new Dictionary<ResourceTypeID, StorageCell>();
        _strict = strict;
        _capacity = capacity;

        foreach (var entry in setup)
            _cells[entry.resourceID] = new StorageCell(entry);
    }

    public bool Add(ResourceTypeID id, int amount)
    {
        if (_strict && !_cells.ContainsKey(id)) return false;
        if (_strict && _cells.Values.Sum(x => x.currentAmount) < _capacity)
            _cells[id].Add(amount);
        return true;
    }

    public bool TrySpend(ResourceTypeID resourceTypeID, int amount)
    {
        return _cells[resourceTypeID].TrySpend(amount);
    }
}
