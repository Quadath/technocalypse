using System;
using UnityEngine;
using System.Collections.Generic;

public class AttackBehaviour: IUnitBehaviour
{
    private readonly Unit unit;
    private int damage;
    private float shootSpeed;
    private float attackRange;
    private float delta = 0f;
    public ITargetable Target { get; private set; } = null;

	private List<Func<bool>> shootRequirements = new List<Func<bool>>();
    
    public AttackBehaviour(Unit unit, int damage, float shootSpeed, float attackRange)
    {
        this.unit = unit;
        this.damage = damage;
        this.shootSpeed = shootSpeed;
        this.attackRange = attackRange;
        
        shootRequirements.Add(() => Target != null);
        shootRequirements.Add(() => delta <= 0);
    }
    public void OnTick(float deltaTime)
    {
	    if (CanShoot())
	    {
		    Attack();
	    }
        if (delta > 0)
            delta -= deltaTime;
        else
			delta = 0;
    }

	public Vector3 GetTargetPosition() {
		if (Target.Transform == null) return Vector3.zero;
		return Target.Transform.position;
	}

	private bool CanShoot()
    {
        foreach (var req in shootRequirements)
        {
            if (!req()) return false; // short-circuit
        }
        return true;
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
