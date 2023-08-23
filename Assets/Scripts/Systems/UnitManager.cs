using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Core;
using Game.Shared;
using Game.Shared.Core;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private readonly List<IUnit> units = new();
    private List<IUnit> selectedUnits = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void MoveCommand(Vector3Int pos)
    {
	    DebugUtil.Log(GetType().Name, $"Move order: {pos} for {selectedUnits.Count} units.", "orange");
        foreach (var unit in selectedUnits)
        {
			if(unit.Player != 0) continue;
            unit.MoveTo(pos);
        }
    }	

	private IUnit GetClosestEnemy(IUnit self, List<IUnit> units, float radius)
	{
    	float radiusSqr = radius * radius;
    	return units
        	.Where(u => u != self 
                    && u.Player != self.Player
                    && (u.Transform.position - self.Transform.position).sqrMagnitude <= radiusSqr)
        	.OrderBy(u => (u.Transform.position - self.Transform.position).sqrMagnitude)
        	.FirstOrDefault();
	}
	
    public void Register(IUnit u)
    {
	    units.Add(u);
        u.AddOnDeathListener(t => Unregister((IUnit)t));
    }

    private void Unregister(IUnit u)
    {
        u.RemoveOnDeathListener(t => Unregister((IUnit)t));
        bool removed = units.Remove(u);
	    selectedUnits.Remove(u);
    }

    // Викликаємо Tick у FixedUpdate — корисно для сумісності з фізикою

    private void FixedUpdate()
    {
        var dt = Time.fixedDeltaTime;
        foreach (var t in units)
        {
            AttackBehaviour attackBehaviour = t.GetBehaviour<AttackBehaviour>();
            if(attackBehaviour != null) {
                ITargetable enemy = GetClosestEnemy(t, units, t.DetectionRange);	
                if(enemy != null) { 	
                    attackBehaviour.SetTarget(enemy);
                }
            }
            t.Tick(dt);
        }
    }

    public void SelectUnits(List<IUnit> units)
    {
	    selectedUnits = units;
    }
}