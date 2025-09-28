using UnityEngine;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private WorldManager manager;
    public UnitData unitData;

    void Start()
    {
        SpawnAt(new Vector3(10, 17, 5));
    }

    public void SpawnAt(Vector3 worldPos)
    {
		GameObject go = Instantiate(unitData.prefab, worldPos, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        Unit unitCore = new Unit("Tank T1", 1, go.transform, rb, unitData.speed);
        unitCore.PathfindingGrid = manager.PathfindingGrid;

        // 1) створюємо core
        var move = new MoveBehaviour(unitCore);
        unitCore.AddBehaviour(move);

        // 2) реєструємо в менеджері
        UnitManager.Instance.Register(unitCore);

        // 3) створюємо view і біндимо
        var view = go.GetComponent<UnitView>();
        view.Bind(unitCore);

        // Приклад: одразу задати шлях (можна задати пізніше)
        // var path = new List<Vector3> {
        //     worldPos,
        //     worldPos + new Vector3(3,0,0),
        //     worldPos + new Vector3(3,0,3)
        // };
        // unitCore.SetMovePath(path);
    }
}