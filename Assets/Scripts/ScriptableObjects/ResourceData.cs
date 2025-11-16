using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Basics/Resource")]
public class ResourceData : ScriptableObject
{
    public ResourceTypeID resourceTypeID;
    public string displayName;
    public Sprite icon;
}
