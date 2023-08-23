using UnityEngine;
using System.Collections.Generic;
public class BuildingSpawner : ServiceConsumer, IBuildingSpawner
{
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private List<BuildingSpawnData> buildings;

    private BuildingGrid _grid;

    void Start()
    {
        //Spawn buildings after world generation
        worldManager.OnGenerated += OnWorldGenerated;
    }

    private void OnWorldGenerated(WorldManager manager)
    {
        _grid = manager.BuildingGrid;
        foreach (var b in buildings)
        {
            SpawnAt(b.Position, b.Rotation, b.buildingData, b.player);
        }
    }

    public void SpawnAt(Vector3Int placePos, Quaternion rot, IBuildingData buildingData, int player)
    {
        //Розрахунок коордиантів будівлі, щоб вона стояла за сіткою
        Vector3 worldPos = placePos + buildingData.Size / 2 - new Vector3(0f, buildingData.Size.y / 2f, 0f);
        GameObject newObj = Instantiate(buildingData.Prefab, worldPos, rot);
        newObj.transform.position = new Vector3(placePos.x, placePos.y, placePos.z);
        
        //Створення DI контейнеру, що передає необхідні сервіси
        var logger = newObj.AddComponent<Logger>();
        var ctx = new Context();
        ctx.Register<ILogger>(logger);
        
        Building b = new Building(buildingData.Name, buildingData.Size, player, buildingData.HitPoints, ctx);
        //Задаємо Origin для будівлі, який буде використовуватись надалі в Core поведінках
        b.SetOrigin(placePos);
        
        foreach (var behaviourData in buildingData.Behaviours) {
            var behaviour = behaviourData.CreateBehaviour(b, Services);
            b.AddBehaviour(behaviour);
        }
        
        _grid.PlaceBuilding(b, placePos.x, placePos.y, placePos.z);
        BuildingView view = newObj.AddComponent<BuildingView>();
        view.Init(b);
        
        newObj.GetComponent<TeamPainter>().Repaint(player);
        newObj.GetComponent<BoxCollider>().enabled = true;
    }
}
