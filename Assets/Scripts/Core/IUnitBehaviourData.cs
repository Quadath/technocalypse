public interface IUnitBehaviourData
{
    UnitBehaviourType Type { get; }
    IUnitBehaviour CreateBehaviour(Unit owner);
}