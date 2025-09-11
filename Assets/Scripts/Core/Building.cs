using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public string Name { get; }
    public int Player { get; }
    public Vector3Int Origin { get; private set; }
    public Vector3Int Size { get; }
    public int HitPoints { get; private set; }
    public int MaxHitPoints { get; private set; }
    public GameObject GameObject;

    private List<IBuildingBehaviour> behaviours = new();

    public Building(string name, Vector3Int size, int player, int hp)
    {
        Name = name;
        Size = size;
        Player = player;
        HitPoints = hp;
        MaxHitPoints = hp;
    }

    public void AddBehaviour(IBuildingBehaviour behaviour)
    {
		behaviours.Add(behaviour);
    }

	public void Tick(float deltaTime) {
		foreach (var b in behaviours)
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