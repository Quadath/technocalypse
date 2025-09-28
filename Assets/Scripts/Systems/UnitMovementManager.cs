using UnityEngine;
using System.Collections.Generic;
public class UnitMovementManager : MonoBehaviour {
// {
//     public PathfindingGrid grid;
//     // private List<UnitController> unitControllers = new List<UnitController>();
//
//     public GameObject UnitPrefab;
//     void Start()
//     {
//         for (int i = 0; i < 2; i++)
//         {
//             {
//                 GameObject go = Instantiate(UnitPrefab, new Vector3(i * 2 + 20, 20, i * 2 + 20), Quaternion.identity);
//                 Rigidbody rb = go.GetComponent<Rigidbody>();
//
//                 Unit unit = new Unit("Tank T1 " + i, 1,  go.transform, rb, 10);
//                 UnitController controller = new UnitController(unit);
//                 UnitView view = go.GetComponent<UnitView>();
//                 view.UnitCore = unit;
//                 controller.unitView = view;
//                 controller.grid = grid;
//                 // unitControllers.Add(controller);
//             }
//             {
//                 GameObject go = Instantiate(UnitPrefab, new Vector3(i * 2 + 40, 20, i * 2 + 60), Quaternion.identity);
//                 Rigidbody rb = go.GetComponent<Rigidbody>();
//
//                 Unit unit = new Unit("Tank T1 " + i, 2,  go.transform, rb, 10);
//                 UnitController controller = new UnitController(unit);
//                 UnitView view = go.GetComponent<UnitView>();
//                 view.UnitCore = unit;
//                 controller.unitView = view;
//                 controller.grid = grid;
//                 // unitControllers.Add(controller);
//             }
//
//             // даємо випадкову ціль
//             // Vector3 target = new Vector3(Random.Range(5, 15), 16, Random.Range(5, 15));
//             // controller.SetTarget(target);
//         }
//     }
//     
//     public void SetT(Vector3 pos)
//     {
//         foreach (var controller in unitControllers)
//         {
//             controller.MoveTo(Vector3Int.RoundToInt(pos));
//         }
//     }
}