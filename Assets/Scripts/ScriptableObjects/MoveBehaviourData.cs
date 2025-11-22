using Game.Core.Behaviours;
using Game.Shared.Core;
using Game.Shared.SO;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveBehaviourData", menuName = "Behaviour/Unit/MoveBehaviourData")]
public class MoveBehaviourData : ScriptableObject, IUnitBehaviourData
{
    public IUnitBehaviour CreateBehaviour(IUnit owner)
    {
        return new MoveBehaviour(owner);
    }
}
