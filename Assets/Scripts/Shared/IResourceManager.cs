public interface IResourceManager
{
    public void AddResource(ResourceTypeID id, int amount);
    public bool TrySpend(ResourceTypeID id, int amount);
}