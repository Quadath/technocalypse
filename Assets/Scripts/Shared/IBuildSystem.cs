public interface IBuildSystem
{
    public void Init(IOrderSystem sys);
    public void SelectBuilding(IBuildingData data);
}