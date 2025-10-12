using UnityEngine;
using System.Collections.Generic;
public class BuildingSpawner : ServiceConsumer, IBuildingSpawner
{
    [SerializeField] private WorldManager worldManager;
    public List<BuildingSpawnData> buildings;
    private BuildingGrid grid;
    
    void Start()
    {
        worldManager.OnGenerated += OnWorldGenerated;
    }

    private void OnWorldGenerated(WorldManager manager)
    {
        grid = manager.BuildingGrid;
        foreach (var b in buildings)
        {
            SpawnAt(b.Position, b.Rotation, b.buildingData, b.player);
        }
    }

    public void SpawnAt(Vector3Int placePos, Quaternion rot, IBuildingData buildingData, int player)
    {
        Vector3 worldPos = placePos + buildingData.Size / 2 - new Vector3(0f, buildingData.Size.y / 2f, 0f);
        Building b = new Building(buildingData.Name, buildingData.Size, player, buildingData.HitPoints);
        b.SetOrigin(placePos);
        foreach (var behaviourData in buildingData.Behaviours) {
            var behaviour = behaviourData.CreateBehaviour(b, Services);
            b.AddBehaviour(behaviour);
        }
        
        grid.PlaceBuilding(b, placePos.x, placePos.y, placePos.z);
        GameObject newObj = Instantiate(buildingData.Prefab, worldPos, rot);
        newObj.GetComponent<TeamPainter>().Repaint(player);
        BuildingView view = newObj.AddComponent<BuildingView>();
        view.Init(b);
        newObj.GetComponent<BoxCollider>().enabled = true;
    }
}
