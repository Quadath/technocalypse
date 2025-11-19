public class ResourceProducer : IBuildingBehaviour
{
    private readonly ResourceTypeID _resource;
    private readonly int _amount;
    private readonly float _interval;
    private readonly IResourceManager _manager;
    
    private float _timer;

    public ResourceProducer(ResourceTypeID resource, int amount, float interval, IResourceManager manager)
    {
        _resource = resource;
        _amount = amount;
        _interval = interval;
        _manager = manager;
    }

    public void OnTick(float deltaTime)
    {
        _timer += deltaTime;
        if (!(_timer >= _interval)) return;
        _timer -= _interval;
        _manager.AddResource(_resource, _amount);
    }
}