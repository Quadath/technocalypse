public interface IBuildSystem
{
    public void Init(IOrderSystem sys);
    public void SetServices(IServiceProvider services);
    public void SelectBuilding(IBuildingData data);
}