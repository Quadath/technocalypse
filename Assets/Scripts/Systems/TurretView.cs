 using Game.Core;
 using Game.Shared.Core;
 using UnityEngine;

 public class TurretView : MonoBehaviour
 {
     [SerializeField] private Transform turret;
     
     private readonly float _rotationSpeed = 75f;
     
     private IUnit _unitCore;
     private bool _facedTarget;

     void Start()
     {
         _unitCore = GetComponent<UnitView>().UnitCore;
         _unitCore.AddOnDeathListener(OnUnitDeath);
         var attackBehaviour = _unitCore.GetBehaviour<AttackBehaviour>(); //as AttackBehaviour;
         attackBehaviour.AddShootRequirement(() => _facedTarget);
     }
     void Update()
     {
         if (_unitCore.GetBehaviour<AttackBehaviour>().Target == null) return;
         var targetPos = _unitCore.GetBehaviour<AttackBehaviour>().GetTargetPosition();
         var direction = targetPos == Vector3.zero ? turret.right : targetPos - turret.position;
         direction.y = 0; // поворот тільки по горизонталі
         if (!(direction.sqrMagnitude > 0.1f)) return;
         var targetRotation = Quaternion.LookRotation(direction);
         targetRotation *= Quaternion.Euler(0f, -90f, 0f);
         turret.rotation = Quaternion.RotateTowards(
             turret.rotation,           // current rotation
             targetRotation,            // desired rotation
             _rotationSpeed * Time.deltaTime  // degrees per frame
         );
         float angle = Quaternion.Angle(turret.rotation, targetRotation);
         _facedTarget = (angle < 1f);
         // Debug.Log($"Angle: {angle}");
     }

     private void OnUnitDeath(ITargetable target)
     {
         target.RemoveOnDeathListener(OnUnitDeath);
         _unitCore = null;
     }
 }