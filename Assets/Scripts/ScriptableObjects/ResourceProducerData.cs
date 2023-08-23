using Game.Core.Behaviours;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Building/ResourceProducerData")]
public class ResourceProducerData : BuildingBehaviourData {
    public ResourceData resource;
    public int amountPerPeriod;
    public float tickInterval;

    public override IBuildingBehaviour CreateBehaviour(IBuilding owner, IServiceProvider services)
    {
        var manager = services.Get<IResourceManager>();
        return new ResourceProducer(owner, resource.resourceTypeID, amountPerPeriod, tickInterval, manager);
    }
}