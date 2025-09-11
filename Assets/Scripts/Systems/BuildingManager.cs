using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {
    private readonly List<Building> buildings = new();

    public void RegisterBuilding(Building b) {
        buildings.Add(b);
    }

    private void Update() {
        float deltaTime = Time.deltaTime;
        foreach (var b in buildings) {
            b.Tick(deltaTime); 
        }
    }
}