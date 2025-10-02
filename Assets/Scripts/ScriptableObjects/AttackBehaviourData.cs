using UnityEngine;

[CreateAssetMenu(fileName = "AttackBehaviourData", menuName = "UnitBehaviour/AttackBehaviourData")]
public class AttackBehaviourData : ScriptableObject, IUnitBehaviourData
{
    [SerializeField] private UnitBehaviourType type;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float shootSpeed;
    
    public UnitBehaviourType Type => type;

    public IUnitBehaviour CreateBehaviour(Unit owner)
    {
        return new AttackBehaviour(owner);
    }
}
