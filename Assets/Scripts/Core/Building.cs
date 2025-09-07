using UnityEngine;

public class Building
{
    public string Name { get; }
    public Vector3Int Origin { get; }
    public Vector3Int Size { get; }
    public int Health { get; private set; }

    public Building(string name, Vector3Int origin, Vector3Int size, int health)
    {
        Name = name;
        Origin = origin;
        Size = size;
        Health = health;
    }

    public void TakeDamage(int dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {

    }
}