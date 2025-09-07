using UnityEngine;

public class TurretView : MonoBehaviour
{
    public Transform Turret;
    public Unit Unit;

    void Start()
    {
        Unit = GetComponent<UnitView>().Unit;
    }
    void Update()
    {
        if (Unit.Target != null)
        {
            Vector3 direction = Unit.Target.Transform.position - Turret.position;
            direction.y = 0; // поворот тільки по горизонталі
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Turret.rotation = Quaternion.Slerp(Turret.rotation, targetRotation, 5f * Time.deltaTime);
            }
        }
    }
}