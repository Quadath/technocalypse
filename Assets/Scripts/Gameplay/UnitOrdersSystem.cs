using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UnitOrdersSystem : MonoBehaviour, IUnitOrdersSystem
{
    private OrderSystem orderSystem;
    //[SerializeField] private UnitMovementManager manager;
    [SerializeField] private Camera cam;
    public void Init(OrderSystem sys)
    {
        orderSystem = sys;
    }

    void Update()
    {
        if (!orderSystem.Active)
            return;
        

        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
                return; // якщо натиснули на UI — не робимо Raycast
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);

        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 50, groundLayerMask))
        {
            UnitManager.Instance.MoveCommand(new Vector3Int(
                Mathf.FloorToInt(hitInfo.point.x),
                Mathf.FloorToInt(hitInfo.point.y),
                Mathf.FloorToInt(hitInfo.point.z)
            ));
        }
    }
}
