using UnityEngine;
using TMPro;


public class ResourceManager : MonoBehaviour, IResourceManager
{
    public ResourceInventory Inventory { get; private set; } = new ResourceInventory();
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        // можна ініціалізувати стартові ресурси
        // Inventory.AddResource("Wood", 100);
        // Inventory.AddResource("Metal", 50);
    }

    public void AddResource(string name, int amount)
    {
        Inventory.AddResource(name, amount);
        text.text = "" + Inventory.GetAmount(name);
    }
}