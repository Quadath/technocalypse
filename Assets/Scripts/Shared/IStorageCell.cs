public interface IStorageCell
{
    ResourceTypeID resourceID { get; }
    int maxCapacity { get; }
    int currentAmount { get; }
    bool Add(int amount);
}
