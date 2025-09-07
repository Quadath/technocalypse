using UnityEngine;
using System.Collections.Generic;
public class UnitMovementManager : MonoBehaviour
{
    private List<UnitController> unitControllers = new List<UnitController>();

    public GameObject UnitPrefab;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(UnitPrefab, new Vector3(i * 2 + 20, 20, i * 2 + 20), Quaternion.identity);
            Rigidbody rb = go.GetComponent<Rigidbody>();

            Unit unit = new Unit("Tank T1 " + i, go.transform, rb, Random.Range(3f, 6f));
            UnitController controller = new UnitController(unit);
            go.GetComponent<UnitView>().Unit = unit;
            unitControllers.Add(controller);

            // даємо випадкову ціль
            // Vector3 target = new Vector3(Random.Range(5, 15), 0, Random.Range(5, 15));
            // controller.SetTarget(target);
        }
    }

    public void Register(UnitController controller)
    {
        unitControllers.Add(controller);
    }

    public void Unregister(UnitController controller)
    {
        unitControllers.Remove(controller);
    }

    void Update()
    {
        foreach (var controller in unitControllers)
        {
            controller.Update();
        }
    }
}