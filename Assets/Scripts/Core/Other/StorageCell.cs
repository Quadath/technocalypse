using System;

public class StorageCell: IStorageCell
{
    public ResourceTypeID resourceID { get; }
    public int maxCapacity { get; }
    public int currentAmount { get; private set; }

    public StorageCell(ResourceTypeID id, int maxCapacity)
    {
        resourceID = id;
        this.maxCapacity = maxCapacity;
    }

    public bool Add(int amount)
    {
        var free = maxCapacity - currentAmount;
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
