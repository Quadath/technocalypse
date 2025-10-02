using UnityEngine;

[CreateAssetMenu(fileName = "MoveBehaviourData", menuName = "UnitBehaviour/MoveBehaviourData")]
public class MoveBehaviourData : ScriptableObject, IUnitBehaviourData
{
    [SerializeField] private UnitBehaviourType type;
    public UnitBehaviourType Type => type;

    public IUnitBehaviour CreateBehaviour(Unit owner)
    {
        return new MoveBehaviour(owner);
    }
}
