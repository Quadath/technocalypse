using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BuildSystem : ServiceConsumer, IBuildSystem
{
    [SerializeField] private WorldManager worldManager;
    private BuildingGrid grid;
    private IServiceProvider services;
    private IOrderSystem orderSystem;
	private IBuildingSpawner spawner;
    private float gridSize = 1f;
    private GameObject previewObject;
	private Vector3Int blockPos;
    private Vector3Int placePos;
    public IBuildingData selectedBuilding;
    public Camera cam;

    public void Init(IOrderSystem sys)
    {
        orderSystem = sys;
        orderSystem.OnStateChanged += HandleStateChange;
    }
    
    void Start()
    {
		spawner = GetComponent<IBuildingSpawner>();
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
            blockPos = new Vector3Int(
                Mathf.FloorToInt((hitInfo.point.x - (hitInfo.normal.x / 2)) / gridSize),
                Mathf.FloorToInt(hitInfo.point.y - (hitInfo.normal.y / 2) / gridSize),
                Mathf.FloorToInt(hitInfo.point.z - (hitInfo.normal.z / 2) / gridSize)
            );

            placePos = blockPos + Vector3Int.RoundToInt(hitInfo.normal);
			//placePos = blockPos + selectedBuilding.Size / 2;
            Building b = new Building(selectedBuilding.Name, selectedBuilding.Size, 0, selectedBuilding.HitPoints);
            if (grid.CanPlaceBuilding(b, placePos.x, placePos.y, placePos.z))
            {
                //DebugDraw.DrawCube(placePos + Vector3.one * 0.5f, 1, new Color(1, 0, 0));
                DebugDraw.DrawCube(placePos + Vector3.one * 0.5f, 1, new Color(0, 1, 1));
				
				Vector3 worldPos = blockPos + selectedBuilding.Size / 2;
                previewObject.transform.position = Vector3.Slerp(previewObject.transform.position, worldPos, 5 * Time.deltaTime);
                previewObject.transform.rotation = Quaternion.identity;
            }

        }
    }

	private void OnDrawGizmos()
	{
		if(!previewObject) return;
		Gizmos.DrawWireSphere(previewObject.transform.position, 0.25f);
	}
    void PlaceBuilding()
    {
        spawner.SpawnAt(placePos, Quaternion.identity, selectedBuilding, 0);
    }
}