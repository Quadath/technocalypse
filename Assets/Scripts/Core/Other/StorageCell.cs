using System;

public class StorageCell: IStorageCell
{
    public ResourceTypeID resourceID { get; }
    public int capacity { get; }
    public int currentAmount { get; private set; }

    public StorageCell(StorageCellDataPair pair)
    {
        resourceID = pair.resourceID;
        capacity = pair.capacity;;
    }

    public bool Add(int amount)
    {
        var free = capacity - currentAmount;
        if (free < amount) return false;
        currentAmount += amount;
        return true;
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0) return true;
        if (amount > currentAmount) return false;
        currentAmount -= amount;
        return true;
    }
}

[Serializable]
public class StorageCellDataPair
{
    public ResourceTypeID resourceID;
    public int capacity;
}
