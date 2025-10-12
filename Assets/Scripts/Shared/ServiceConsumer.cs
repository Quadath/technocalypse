using UnityEngine;

public abstract class ServiceConsumer : MonoBehaviour
{
    protected IServiceProvider Services { get; private set; }

    public virtual void SetServices(IServiceProvider services)
    {
        Services = services;
        OnServicesSet();
    }

    protected virtual void OnServicesSet() {}
}
