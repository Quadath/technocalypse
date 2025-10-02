public enum UnitBehaviourType
{
    MoveBehaviour,
	AttackBehaviour,
}

public static class UnitBehaviourFactory
{
    public static void AddBehaviour(Unit unit, UnitBehaviourType type)
    {
        switch (type)
        {
            case UnitBehaviourType.MoveBehaviour:
                unit.AddBehaviour(new MoveBehaviour(unit));
                break;
            // case UnitBehaviourType.Patrol:
            //     unit.AddBehaviour(new PatrolBehaviour());
            //     break;
            // case UnitBehaviourType.GatherResources:
            //     unit.AddBehaviour(new GatherBehaviour());
            //     break;
        }
    }
}