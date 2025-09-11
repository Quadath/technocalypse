public interface IBuildSystem
{
    public void Init(OrderSystem sys);
    public void SetServices(IServiceProvider services);
}