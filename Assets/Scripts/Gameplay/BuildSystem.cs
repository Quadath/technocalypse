using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BuildSystem : MonoBehaviour, IBuildSystem
{
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private WorldManager worldManager;

    private BuildingGrid grid;
    private IServiceProvider services;
    private OrderSystem orderSystem;
    private float gridSize = 1f;
    private GameObject previewObject;
    public Camera cam;
    public BuildingData selectedBuilding;
    private Vector3Int placePos;
    public void Init(OrderSystem sys)
    {
        orderSystem = sys;
    }
    void Start()
    {
        grid = worldManager.BuildingGrid;
    }

    public void SetServices(IServiceProvider services)
    {
        this.services = services;
    }

    void Update()
    {
        if (!orderSystem.Active)
            return;

        if (previewObject != null)
        {
            FollowMouse();
        }
        else
        {
            previewObject = Instantiate(buildingData.prefab);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            PlaceBuilding();
        }
    }
    public UnitMovementManager mang;
    void FollowMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);

        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 50, groundLayerMask))
        {
            // return hitInfo.point;

            // координати блоку, в який влучили
            Vector3Int blockPos = new Vector3Int(
                Mathf.FloorToInt((hitInfo.point.x - (hitInfo.normal.x / 2)) / gridSize),
                Mathf.FloorToInt(hitInfo.point.y - (hitInfo.normal.y / 2) / gridSize),
                Mathf.FloorToInt(hitInfo.point.z - (hitInfo.normal.z / 2) / gridSize)
            );

            placePos = blockPos + Vector3Int.RoundToInt(hitInfo.normal);
            Building b = new Building(buildingData.Name, buildingData.Size, 1, buildingData.HitPoints);
            if (grid.CanPlaceBuilding(b, placePos.x, placePos.y, placePos.z))
            {
                DebugDraw.DrawCube(placePos + Vector3.one * 0.5f, 1, new Color(1, 0, 0));

                previewObject.transform.position = Vector3.Slerp(previewObject.transform.position, placePos, 5 * Time.deltaTime);
                previewObject.transform.rotation = Quaternion.identity;
            }

        }
    }

    void PlaceBuilding()
    {
        Building b = new Building(buildingData.Name, buildingData.Size, 1, buildingData.HitPoints);
        foreach (var behaviourData in selectedBuilding.behaviours) {
            var behaviour = behaviourData.CreateBehaviour(b, services);
            b.AddBehaviour(behaviour);
        }
        
        grid.PlaceBuilding(b, placePos.x, placePos.y, placePos.z);
        GameObject newObj = Instantiate(buildingData.prefab, placePos, previewObject.transform.rotation);
        BuildingView view = newObj.AddComponent<BuildingView>();
        view.Init(b);
        newObj.GetComponent<BoxCollider>().enabled = true;
    }
}