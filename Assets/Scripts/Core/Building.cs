using System;
using System.Collections.Generic;
using Game.Shared.Core;
using UnityEngine;

public class Building : IBuilding
{
    private readonly List<IBuildingBehaviour> _behaviours = new();
    
    private event Action<IBuilding> OnDeath;
    private int HitPoints { get; set; }
    
    public Transform Transform { get; }
    public string Name { get; private set; }
    public int Player { get; }
    public Vector3Int Origin { get; private set; }
    public Vector3Int Size { get; }
    public int MaxHitPoints { get; private set; }
    public ILogger Logger { get; }



    public IBuilding GetTarget() => this;
    public void AddBehaviour(IBuildingBehaviour behaviour) => _behaviours.Add(behaviour);
    public void AddOnDeathListener(Action<ITargetable> listener) => OnDeath += listener;
    public void RemoveOnDeathListener(Action<ITargetable> listener) => OnDeath -= listener;
    
    public Building(string name, Vector3Int size, int player, int hp, IContext context = null)
    {
        Name = name;
        Size = size;
        Player = player;
        HitPoints = hp;
        MaxHitPoints = hp;
        Logger = context?.Resolve<ILogger>();
    }

    
    public T GetBehaviour<T>() where T : class, IBuildingBehaviour
    {
        foreach (var b in _behaviours)
            if (b is T t)
                return t;
        return null;
    }
	public void Tick(float deltaTime) {
		foreach (var b in _behaviours)
			b.OnTick(deltaTime);
	}	

    public void TakeDamage(int dmg)
    {
        HitPoints -= dmg;
        if (HitPoints <= 0)
        {
            Destroy();
        }
    }

	public void SetOrigin(Vector3Int origin) {
		Origin = origin;
	}

    private void Destroy()
    {

    }
}