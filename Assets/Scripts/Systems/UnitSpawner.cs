using UnityEngine;
using System.Collections.Generic;
using Game.Core;
using Game.Shared.Core;
using Game.Shared.SO;
using Game.Shared.Systems;
using Game.Systems;

public class UnitSpawner : MonoBehaviour, IUnitSpawner
{
    [SerializeField] private WorldManager manager;
    
	public List<UnitSpawnData> units;

    void Start()
    {
        foreach (var u in units)
        {
			SpawnAt(u.Position, u.Rotation, u.unitData, u.player);
        }
    }

    public void SpawnAt(Vector3 worldPos, Quaternion rot, IUnitData unitData, int player)
    {
		var go = Instantiate(unitData.Prefab, worldPos, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody>();
        var ctx = new Context();
        ctx.Register<ILogger>(go.AddComponent<Logger>());
        
        IUnit unitCore = new Unit(unitData.UnitName, player, go.transform, rb, unitData.Speed, unitData.HitPoints, ctx)
            {
                PathfindingGrid = manager.PathfindingGrid
            };
        LeakTracker.Register(unitCore);
        
        // 1) створюємо core
        //var move = new MoveBehaviour(unitCore);
        //unitCore.AddBehaviour(move);
        foreach (var behaviour in unitData.Behaviours) 
        	unitCore.AddBehaviour(behaviour.CreateBehaviour(unitCore));

        // 2) реєструємо в менеджері
        UnitManager.Instance.Register(unitCore);

        // 3) створюємо view і біндимо
        var view = go.AddComponent<UnitView>();
		if (unitCore.GetBehaviour<AttackBehaviour>() != null) {
			unitCore.GetBehaviour<AttackBehaviour>().OnShoot += view.OnShootVisual;
		}
        view.Bind(unitCore);
		go.GetComponent<TeamPainter>().Repaint(player);
    }
}