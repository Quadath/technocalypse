using UnityEngine;
public abstract class BuildingBehaviourData: ScriptableObject, IBuildingBehaviourData {
    public abstract IBuildingBehaviour CreateBehaviour(IBuilding owner, IServiceProvider services);
}