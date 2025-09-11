public class ResourceProducer : IBuildingBehaviour
{
    private string resource;
    private int amount;
    private float interval;
    private float timer;
    private IResourceManager manager;

    public ResourceProducer(string resource, int amount, float interval, IResourceManager manager)
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

public interface IResourceManager
{
    public void AddResource(string name, int amount);
}
