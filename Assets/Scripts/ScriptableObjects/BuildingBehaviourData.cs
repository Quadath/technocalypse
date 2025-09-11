using UnityEngine;
public abstract class BuildingBehaviourData : ScriptableObject {
    public abstract IBuildingBehaviour CreateBehaviour(Building owner, IServiceProvider services);
}