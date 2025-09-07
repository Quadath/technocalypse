using UnityEngine;
public class UnitController
{
    private Unit unit;
    private Vector3? target;

    public UnitController(Unit unit)
    {
        this.unit = unit;
    }

    public void SetTarget(Vector3 position)
    {
        target = position;
    }

    public void Update()
    {
        if (target == null) return;

        Vector3 dir = (target.Value - unit.Transform.position).normalized;
        Vector3 newPos = unit.Transform.position + dir * unit.Speed * Time.deltaTime;

        unit.Rigidbody.MovePosition(newPos);

        if (Vector3.Distance(unit.Transform.position, target.Value) < 0.2f)
            target = null; // досягли
    }
}