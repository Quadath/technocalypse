public class ResourceProducer : IBuildingBehaviour
{
    private ResourceTypeID resource;
    private int amount;
    private float interval;
    private float timer;
    private IResourceManager manager;

    public ResourceProducer(ResourceTypeID resource, int amount, float interval, IResourceManager manager)
    {
        this.resource = resource;
        this.amount = amount;
        this.interval = interval;
        this.manager = manager;
    }

    public void OnTick(float deltaTime)
    {
        timer += deltaTime;
        if (timer >= interval)
        {
            timer -= interval;
            manager.AddResource(resource, amount);
        }
    }
}