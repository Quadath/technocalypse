using UnityEngine;
using System.Collections.Generic;

//provides services for systems
public class ServiceProviderView : MonoBehaviour {
    public static IServiceProvider Services { get; private set; }
	[SerializeField] private List<ServiceConsumer> consumers;

    private void Awake() {
        var provider = new ServiceProvider();
        
        // реєструємо
        var resourceManager = GetComponent<ResourceManager>();
        var unitSpawner = GetComponent<UnitSpawner>();
        provider.Register<IResourceManager>(resourceManager);
		provider.Register<IUnitSpawner>(unitSpawner);
        // provider.Register<IUnitFinder>(unitFinder);

        Services = provider;
        foreach(var consumer in consumers) {
			consumer.SetServices(Services);
		} 
    }
}
