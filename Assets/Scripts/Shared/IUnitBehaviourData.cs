using Game.Shared.Core;

namespace Game.Shared.SO
{
    public interface IUnitBehaviourData
    {
        IUnitBehaviour CreateBehaviour(IUnit owner);
    }
}
