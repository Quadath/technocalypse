using UnityEngine;

public class AttackBehaviour: IUnitBehaviour
{
    private readonly Unit unit;
    private int damage;
    private float shootSpeed;
    private float attackRange;
    private float delta = 0f;
    private ITargetable Target = null;
    
    public AttackBehaviour(Unit unit, int damage, float shootSpeed, float attackRange)
    {
        this.unit = unit;
        this.damage = damage;
        this.shootSpeed = shootSpeed;
        this.attackRange = attackRange;
    }
    public void OnTick(float deltaTime)
    {
        if (delta > 0)
        {
            delta -= deltaTime;
        } else {
			if(Target != null) {
				Attack();
			} else {
				delta = 0;
			}
		}		
    }

	private void Attack()
 	{
		Target.TakeDamage(damage);
		//unit.DebugMessage($"<color=cyan>[{"AttackBehaviour"}]</color>: Attacking");
		delta = 1 / shootSpeed;
	}
    public void SetTarget(ITargetable target)
    {
		if (target == Target) return;
        if (target != null)
        {
            Target = target;
            unit.DebugMessage($"<color=cyan>[{"AttackBehaviour"}]</color>: Target set");
        }
        else return;
    }

    public void ResetTarget()
    {
        Target = null;
    }
}
