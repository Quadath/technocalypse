using UnityEngine;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private WorldManager manager;

	public List<UnitSpawnData> units;

    void Start()
    {
        //SpawnAt(new Vector3(10, 17, 5));
		foreach (var u in units)
        {
			SpawnAt(u.Position, u.Rotation, u.unitData, u.player);
        }
    }

    public void SpawnAt(Vector3 worldPos, Quaternion rot, UnitData unitData, int player)
    {
		GameObject go = Instantiate(unitData.prefab, worldPos, Quaternion.identity);
		go.GetComponent<TeamPainter>().Repaint(player);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        Unit unitCore = new Unit(unitData.name, player, go.transform, rb, unitData.speed);
        unitCore.PathfindingGrid = manager.PathfindingGrid;

        // 1) створюємо core
        //var move = new MoveBehaviour(unitCore);
        //unitCore.AddBehaviour(move);
 		foreach (var behaviourType in unitData.behaviours)
        	UnitBehaviourFactory.AddBehaviour(unitCore, behaviourType);

        // 2) реєструємо в менеджері
        UnitManager.Instance.Register(unitCore);

        // 3) створюємо view і біндимо
        var view = go.GetComponent<UnitView>();
        view.Bind(unitCore);
    }
}