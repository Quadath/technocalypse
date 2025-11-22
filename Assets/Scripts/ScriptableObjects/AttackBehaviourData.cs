using Game.Shared;
using UnityEngine;
using Game.Shared.Core;
using Game.Shared.SO;

[CreateAssetMenu(fileName = "AttackBehaviourData", menuName = "Behaviour/Unit/AttackBehaviourData")]
public class AttackBehaviourData : ScriptableObject, IUnitBehaviourData
{
    [SerializeField] private int damage;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float attackRange;
    

    public IUnitBehaviour CreateBehaviour(IUnit owner)
    {
        return new AttackBehaviour(owner, damage, shootSpeed, attackRange);
    }
}
