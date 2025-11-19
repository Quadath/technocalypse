using System;
using UnityEngine;
using System.Collections.Generic;

public class AttackBehaviour: IUnitBehaviour
{
    private readonly int _damage;
    private readonly float _shootSpeed;
    private readonly float _attackRange;
	private readonly List<Func<bool>> _shootRequirements;
    
    private float _delta;
    
    public ITargetable Target { get; private set; }
	public event Action<AttackBehaviour> OnShoot;

    
    public AttackBehaviour(int damage, float shootSpeed, float attackRange)
    {
        _damage = damage;
        _shootSpeed = shootSpeed;
        _attackRange = attackRange;
        _shootRequirements = new List<Func<bool>>();
        
        AddShootRequirement(() => Target != null);
        AddShootRequirement(() => _delta <= 0);
    }
    public void OnTick(float deltaTime)
    {
	    if (CanShoot())
	    {
		    Attack();
	    }
        if (_delta > 0)
            _delta -= deltaTime;
        else
			_delta = 0;
    }

	public Vector3 GetTargetPosition()
    {
        return Target == null ? Vector3.zero : Target.Transform.position;
    }

	public void AddShootRequirement(Func<bool> callback)
	{
		_shootRequirements.Add(callback);
	}

	private bool CanShoot()
    {
        foreach (var req in _shootRequirements)
        {
            if (!req()) return false; // short-circuit
        }
        return true;
    }

	private void Attack()
 	{
		Target.TakeDamage(_damage);
		//_unit.DebugMessage($"<color=cyan>[{"AttackBehaviour"}]</color>: Attacking");
		_delta = 1 / _shootSpeed;
		OnShoot?.Invoke(this);
	}
    public void SetTarget(ITargetable target)
    {
		if (target == Target) return;
        if (target == null) return;
        Target = target;
        // _unit.DebugMessage(GetType().DisplayedName, "Target set");
    }

    public void ResetTarget()
    {
        Target = null;
    }
}
