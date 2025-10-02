public interface IBuildSystem
{
    public void Init(OrderSystem sys);
    public void SetServices(IServiceProvider services);
    public void SelectBuilding(BuildingData data);
}