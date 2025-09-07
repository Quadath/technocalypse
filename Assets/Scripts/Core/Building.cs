using UnityEngine;

public class Building
{
    public string Name { get; }
    public int Player { get; }
    public Vector3Int Origin { get; }
    public Vector3Int Size { get; }
    public int HitPoints { get; private set; }
    public int MaxHitPoints { get; private set; }
    public GameObject GameObject;

    public Building(string name, Vector3Int size, int player, int hp)
    {
        Name = name;
        Size = size;
        Player = player;
        HitPoints = hp;
        MaxHitPoints = hp;
    }

    public void TakeDamage(int dmg)
    {
        HitPoints -= dmg;
        if (HitPoints <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {

    }
}