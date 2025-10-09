using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private readonly List<Unit> units = new();
    private List<Unit> selectedUnits = new();

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

	public Unit GetClosestEnemy(Unit self, List<Unit> units, float radius)
	{
    	float radiusSqr = radius * radius;
    	return units
        	.Where(u => u != self 
                    && u.Player != self.Player
                    && (u.Transform.position - self.Transform.position).sqrMagnitude <= radiusSqr)
        	.OrderBy(u => (u.Transform.position - self.Transform.position).sqrMagnitude)
        	.FirstOrDefault();
	}
	
    public void Register(Unit u)
    {
	    units.Add(u);
	    u.AddOnDeathListener((Unit) => Unregister(u));
    }

    public void Unregister(Unit u) => units.Remove(u);

    // Викликаємо Tick у FixedUpdate — корисно для сумісності з фізикою

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        for (int i = 0; i < units.Count; i++) {
			AttackBehaviour attackBehaviour = units[i].GetBehaviour<AttackBehaviour>();
			if(attackBehaviour != null) {
				ITargetable enemy = GetClosestEnemy(units[i], units, units[i].DetectionRange);	
				if(enemy != null) { 	
					attackBehaviour.SetTarget(enemy);
				}
			}
            units[i].Tick(dt);
		}
    }

    public void SelectUnits(List<Unit> units)
    {
	    selectedUnits = units;
    }
}