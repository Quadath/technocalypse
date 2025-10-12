public interface IResourceManager
{
    public void AddResource(string name, int amount);
    public bool TrySpend(string name, int amount);
}