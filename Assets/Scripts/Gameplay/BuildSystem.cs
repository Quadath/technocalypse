using UnityEngine;
using UnityEngine.InputSystem;

public class BuildSystem : MonoBehaviour
{
    public Camera cam;
    public GameObject buildingPrefab;

    private GameObject previewObject;

    void Start()
    {
        previewObject = Instantiate(buildingPrefab);
        previewObject.GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        Vector3 pos = FollowMouse();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (pos != Vector3.zero)
            {
                mang.SetT(pos);
            }
            PlaceBuilding();
        }
    }
    public UnitMovementManager mang;
    Vector3 FollowMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point;
            float gridSize = 1f;

            // координати блоку, в який влучили
            Vector3Int blockPos = new Vector3Int(
                Mathf.FloorToInt((hitInfo.point.x - (hitInfo.normal.x / 2)) / gridSize),
                Mathf.FloorToInt(hitInfo.point.y - (hitInfo.normal.y / 2) / gridSize),
                Mathf.FloorToInt(hitInfo.point.z - (hitInfo.normal.z / 2) / gridSize)
            );

            // беремо сусідній блок у напрямку нормалі
            Vector3Int placePos = blockPos + Vector3Int.RoundToInt(hitInfo.normal);
            Debug.Log(hitInfo.point);

            // центр блоку
            Vector3 snappedPos = placePos;

            // previewObject.transform.position = snappedPos;
            previewObject.transform.rotation = Quaternion.identity;

        }
        else
        {
            return Vector3.zero;
        }
    }

    void PlaceBuilding()
    {
        // GameObject newObj = Instantiate(buildingPrefab, previewObject.transform.position, previewObject.transform.rotation);
        // newObj.GetComponent<BoxCollider>().enabled = true;
    }
}