using System;
using UnityEngine;

public class BuildingView : MonoBehaviour, IBuildingView {
    private Building core;

    [Obsolete("Obsolete")]
    public void Init(Building building) {
        core = building;
        FindObjectOfType<BuildingManager>().RegisterBuilding(core);
    }
}