using UnityEngine;

public class BuildSystemCurrentBuildingSwitcher : MonoBehaviour
{
    [SerializeField] private BuildingData _buildingData;
    [SerializeField] private GameObject _buildSystem;
    public void OnClick()
    {
        _buildSystem.GetComponent<IBuildSystem>().SelectBuilding(_buildingData);
    }
}
