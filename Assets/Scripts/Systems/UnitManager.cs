using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private readonly List<Unit> units = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void MoveCommand(Vector3Int pos)
    {
        foreach (var unit in units)
        {
            Debug.Log(pos);
			for (int i = 0; i < units.Count; i++)
            	units[i].MoveTo(pos);
        }
    }

    public void Register(Unit u) => units.Add(u);
    public void Unregister(Unit u) => units.Remove(u);

    // Викликаємо Tick у FixedUpdate — корисно для сумісності з фізикою
    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        for (int i = 0; i < units.Count; i++)
            units[i].Tick(dt);
    }
}