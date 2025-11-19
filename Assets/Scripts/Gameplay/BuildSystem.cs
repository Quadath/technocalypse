using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BuildSystem : MonoBehaviour, IBuildSystem
{
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private Camera cam;
    
    private readonly float _gridSize = 1f;
    
    private BuildingGrid _grid;
    private IOrderSystem _orderSystem;
	private IBuildingSpawner _spawner;
    private GameObject _previewObject;
	private Vector3Int _blockPos;
    private Vector3Int _placePos;
    private IBuildingData _selectedBuilding;

    public void Init(IOrderSystem sys)
    {
        _orderSystem = sys;
        _orderSystem.OnStateChanged += HandleStateChange;
    }
    
    void Start()
    {
		_spawner = GetComponent<IBuildingSpawner>();
        _grid = worldManager.BuildingGrid;
    }

    private void HandleStateChange(bool active)
    {
        if (active)
        {
            _previewObject = SpawnPreviewObject();
        }
        else
        {
            Destroy(_previewObject);
        }
    }

    public void SelectBuilding(IBuildingData data)
    {
        _selectedBuilding = data;
        Debug.Log("Selected: " + _selectedBuilding.Name);
        Destroy(_previewObject);
        _previewObject = SpawnPreviewObject();
    }

    void Update()
    {
        if (!_orderSystem.IsActive)
            return;

        if (_previewObject)
        {
            FollowMouse();
        }

        if (!Mouse.current.leftButton.wasPressedThisFrame) return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        PlaceBuilding();
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
            _blockPos = new Vector3Int(
                Mathf.FloorToInt((hitInfo.point.x - (hitInfo.normal.x / 2)) / _gridSize),
                Mathf.FloorToInt(hitInfo.point.y - (hitInfo.normal.y / 2) / _gridSize),
                Mathf.FloorToInt(hitInfo.point.z - (hitInfo.normal.z / 2) / _gridSize)
            );

            _placePos = _blockPos + Vector3Int.RoundToInt(hitInfo.normal);
			//_placePos = _blockPos + _selectedBuilding.Size / 2;
            Building b = new Building(_selectedBuilding.Name, _selectedBuilding.Size, 0, _selectedBuilding.HitPoints);
            if (!_grid.CanPlaceBuilding(b, _placePos.x, _placePos.y, _placePos.z)) return;
            //DebugDraw.DrawCube(_placePos + Vector3.one * 0.5f, 1, new Color(1, 0, 0));
            DebugDraw.DrawCube(_placePos + Vector3.one * 0.5f, 1, new Color(0, 1, 1));
				
            Vector3 worldPos = _blockPos + _selectedBuilding.Size / 2;
            _previewObject.transform.position = Vector3.Slerp(_previewObject.transform.position, worldPos, 5 * Time.deltaTime);
            _previewObject.transform.rotation = Quaternion.identity;
        }
    }

    private GameObject SpawnPreviewObject()
    {
        var obj = Instantiate(_selectedBuilding.Prefab, _placePos, Quaternion.identity);
        obj.GetComponent<TeamPainter>().Repaint(0);
        return obj;
    }
	private void OnDrawGizmos()
	{
		if(!_previewObject) return;
		Gizmos.DrawWireSphere(_previewObject.transform.position, 0.25f);
	}
    private void PlaceBuilding()
    {
        _spawner.SpawnAt(_placePos, Quaternion.identity, _selectedBuilding, 0);
    }
}