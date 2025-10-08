using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BuildSystem : MonoBehaviour, IBuildSystem
{
    [SerializeField] private WorldManager worldManager;
    private BuildingGrid grid;
    private IServiceProvider services;
    private IOrderSystem orderSystem;
    private float gridSize = 1f;
    private GameObject previewObject;
    public Camera cam;
    public IBuildingData selectedBuilding;
    private Vector3Int placePos;
    public void Init(IOrderSystem sys)
    {
        orderSystem = sys;
        orderSystem.OnStateChanged += HandleStateChange;
    }
    
    void Start()
    {
        grid = worldManager.BuildingGrid;
    }

    private void HandleStateChange(bool active)
    {
        if (active)
        { 
            
        }
        else
        {
            Destroy(previewObject);
        }
        // Debug.Log($"BuildSystem is now {(active ? "Active" : "Inactive")}");
    }
    
    public void SetServices(IServiceProvider services)
    {
        this.services = services;
    }

    public void SelectBuilding(IBuildingData data)
    {
        selectedBuilding = data;
        Debug.Log("Selected: " + selectedBuilding.Name);
        Destroy(previewObject);
    }

    void Update()
    {
        if (!orderSystem.IsActive)
            return;

        if (previewObject != null)
        {
            FollowMouse();
        }
        else
        {
            previewObject = Instantiate(selectedBuilding.Prefab, placePos, Quaternion.identity);
            previewObject.GetComponent<TeamPainter>().Repaint(0);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            PlaceBuilding();
        }
    }
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
            Building b = new Building(selectedBuilding.Name, selectedBuilding.Size, 1, selectedBuilding.HitPoints);
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
        Building b = new Building(selectedBuilding.Name, selectedBuilding.Size, 1, selectedBuilding.HitPoints);
        foreach (var behaviourData in selectedBuilding.Behaviours) {
            var behaviour = behaviourData.CreateBehaviour(b, services);
            b.AddBehaviour(behaviour);
        }
        
        grid.PlaceBuilding(b, placePos.x, placePos.y, placePos.z);
        GameObject newObj = Instantiate(selectedBuilding.Prefab, placePos, previewObject.transform.rotation);
        newObj.GetComponent<TeamPainter>().Repaint(0);
        BuildingView view = newObj.AddComponent<BuildingView>();
        view.Init(b);
        newObj.GetComponent<BoxCollider>().enabled = true;
    }
}