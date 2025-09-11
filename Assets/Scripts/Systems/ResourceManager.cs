using UnityEngine;
public class ResourceManager : MonoBehaviour, IResourceManager
{
    public ResourceInventory Inventory { get; private set; } = new ResourceInventory();

    private void Awake()
    {
        // можна ініціалізувати стартові ресурси
        // Inventory.AddResource("Wood", 100);
        // Inventory.AddResource("Metal", 50);
    }

    public void AddResource(string name, int amount)
    {
        Inventory.AddResource(name, amount);
        Debug.Log(Inventory.GetAmount(name));
    }
}