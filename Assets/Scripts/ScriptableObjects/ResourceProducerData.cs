using UnityEngine;

[CreateAssetMenu(menuName = "Game/Behaviours/Resource Producer")]
public class ResourceProducerData : BuildingBehaviourData {
    public ResourceData resource;
    public int amountPerPeriod;
    public float tickInterval;

    public override IBuildingBehaviour CreateBehaviour(Building owner, IServiceProvider services)
    {
        var manager = services.Get<IResourceManager>();
        return new ResourceProducer(resource.name, amountPerPeriod, tickInterval, manager);
    }
}