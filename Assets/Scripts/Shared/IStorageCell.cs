public interface IStorageCell
{
    ResourceTypeID resourceID { get; }
    int capacity { get; }
    int currentAmount { get; }
    bool Add(int amount);
}
