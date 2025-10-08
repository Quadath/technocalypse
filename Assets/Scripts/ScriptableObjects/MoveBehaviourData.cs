using UnityEngine;

[CreateAssetMenu(fileName = "MoveBehaviourData", menuName = "Behaviour/Unit/MoveBehaviourData")]
public class MoveBehaviourData : ScriptableObject, IUnitBehaviourData
{
    public IUnitBehaviour CreateBehaviour(Unit owner)
    {
        return new MoveBehaviour(owner);
    }
}
