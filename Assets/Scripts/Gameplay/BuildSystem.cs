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
        FollowMouse();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlaceBuilding();
        }
    }

    void FollowMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            float gridSize = 1f;

            // координати блоку, в який влучили
            Vector3Int blockPos = new Vector3Int(
                Mathf.FloorToInt((hitInfo.point.x - (hitInfo.normal.x / 2)) / gridSize),
                Mathf.FloorToInt(hitInfo.point.y - (hitInfo.normal.y / 2)/ gridSize),
                Mathf.FloorToInt(hitInfo.point.z - (hitInfo.normal.z / 2) / gridSize)
            );

            // беремо сусідній блок у напрямку нормалі
            Vector3Int placePos = blockPos + Vector3Int.RoundToInt(hitInfo.normal);
            Debug.Log(hitInfo.point);
            
            // центр блоку
            Vector3 snappedPos = placePos;

            previewObject.transform.position = snappedPos;
            previewObject.transform.rotation = Quaternion.identity;
        }
    }

    void PlaceBuilding()
    {
        GameObject newObj = Instantiate(buildingPrefab, previewObject.transform.position, previewObject.transform.rotation);
        newObj.GetComponent<BoxCollider>().enabled = true;
    }
}