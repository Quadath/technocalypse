using UnityEngine;

public class Building
{
    public string Name { get; }
    public Vector3Int Origin { get; }
    public Vector3Int Size { get; }
    public int HitPoints { get; private set; }

    public Building(string name, Vector3Int origin, Vector3Int size, int hp)
    {
        Name = name;
        Origin = origin;
        Size = size;
        HitPoints = hp;
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