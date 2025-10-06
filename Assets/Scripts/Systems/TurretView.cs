 using UnityEngine;

 public class TurretView : MonoBehaviour
 {
     public Transform Turret;
     public Unit Unit;

     void Start()
     {
         Unit = GetComponent<UnitView>().UnitCore;
     }
     void Update()
     {
         if (Unit.GetBehaviour<AttackBehaviour>().Target != null)
         {
             Vector3 direction = Unit.GetBehaviour<AttackBehaviour>().GetTargetPosition() - Turret.position;
             direction.y = 0; // поворот тільки по горизонталі
             if (direction.sqrMagnitude > 0.01f)	
             {
                 Quaternion targetRotation = Quaternion.LookRotation(direction);
				 targetRotation *= Quaternion.Euler(0f, -90f, 0f);
                 Turret.rotation = Quaternion.Slerp(Turret.rotation, targetRotation, 5f * Time.deltaTime);
             }
         }
     }
 }