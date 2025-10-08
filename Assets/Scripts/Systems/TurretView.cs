 using UnityEngine;

 public class TurretView : MonoBehaviour
 {
     public Transform Turret;
     public Unit Unit;
     private bool facedTarget = false;
     private float rotationSpeed = 75f;

     void Start()
     {
         Unit = GetComponent<UnitView>().UnitCore;
         AttackBehaviour attackBehaviour = Unit.GetBehaviour<AttackBehaviour>() as AttackBehaviour;
         attackBehaviour.AddShootRequirement(() => facedTarget);
     }
     void Update()
     {
         if (Unit.GetBehaviour<AttackBehaviour>().Target != null)
         {
             Vector3 direction = Unit.GetBehaviour<AttackBehaviour>().GetTargetPosition() - Turret.position;
             direction.y = 0; // поворот тільки по горизонталі
             if (direction.sqrMagnitude > 0.1f)	
             {
                 Quaternion targetRotation = Quaternion.LookRotation(direction);
				 targetRotation *= Quaternion.Euler(0f, -90f, 0f);
                 Turret.rotation = Quaternion.RotateTowards(
                     Turret.rotation,           // current rotation
                     targetRotation,            // desired rotation
                     rotationSpeed * Time.deltaTime  // degrees per frame
                 );
                 float angle = Quaternion.Angle(Turret.rotation, targetRotation);
                 if (angle > 1f)
                 {
                     facedTarget = false;
                 }
                 else
                 {
                     facedTarget = true;
                 }
                 // Debug.Log($"Angle: {angle}");
             }
         }
     }
 }