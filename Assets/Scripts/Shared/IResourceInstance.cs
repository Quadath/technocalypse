using UnityEngine;

public interface IResourceInstance
{
    ResourceTypeID resourceTypeID { get; }
    int amount { get; }
    public void Add(int value);
    public bool TrySpend(int value);
}
