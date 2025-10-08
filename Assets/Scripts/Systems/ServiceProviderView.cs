using UnityEngine;

//provides services for systems
public class ServiceProviderView : MonoBehaviour {
    public static IServiceProvider Services { get; private set; }

    private void Awake() {
        var provider = new ServiceProvider();
        
        // реєструємо
        var resourceManager = GetComponent<ResourceManager>();
        provider.Register<IResourceManager>(resourceManager);
        // provider.Register<IUnitFinder>(unitFinder);

        Services = provider;
        GetComponent<IBuildSystem>().SetServices(Services);
    }
}
