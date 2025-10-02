using UnityEngine;

public class AttackBehaviour: IUnitBehaviour
{
    private readonly Unit unit;
    public AttackBehaviour(Unit unit)
    {
        this.unit = unit;
    }
    public void OnTick(float deltaTime)
    {
        unit.DebugMessage("Attack is working");
    }
}
