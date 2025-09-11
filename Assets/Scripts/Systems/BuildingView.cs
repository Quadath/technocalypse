using UnityEngine;

public class BuildingView : MonoBehaviour {
    private Building core;

    public void Init(Building building) {
        core = building;
        FindObjectOfType<BuildingManager>().RegisterBuilding(core);
    }
}