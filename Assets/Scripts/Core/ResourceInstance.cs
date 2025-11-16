public class ResourceInstance: IResourceInstance
{
    public ResourceTypeID resourceTypeID { get; }
    public int amount { get; private set; }

    public ResourceInstance(ResourceTypeID id, int amount = 0)
    {
        resourceTypeID = id;
        this.amount = amount;
    }

    public void Add(int value) => amount += value;
    public bool TrySpend(int value)
    {
        if (amount < value) return false;
        amount -= value;
        return true;
    }
}