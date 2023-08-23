using Game.Shared.Core;

namespace Game.Core.Behaviours
{ 
    public class ResourceProducer : IBuildingBehaviour
    {
        private readonly ResourceTypeID _resource;
        private readonly int _amount;
        private readonly float _interval;
        private readonly IResourceManager _manager;
        private readonly StorageBehaviour _storage;
        
        private float _timer;

        public ResourceProducer(IBuilding parent, ResourceTypeID resource, int amount, float interval, IResourceManager manager)
        {
            _resource = resource;
            _amount = amount;
            _interval = interval;
            _manager = manager;
            _storage = parent.GetBehaviour<StorageBehaviour>();
        }

        public void OnTick(float deltaTime)
        {
            _timer += deltaTime;
            if (!(_timer >= _interval)) return;
            _timer -= _interval;
            _manager.AddResource(_resource, _amount);
        }
    }
}
