using UnityEngine;
using UnityEngine.UI;

public class OrderSystemsManager : MonoBehaviour
{
    private IBuildSystem buildSystem;
    private IUnitOrdersSystem unitOrderSystem;

    [SerializeField] private MonoBehaviour buildSystemComponent;
    [SerializeField] private MonoBehaviour unitOrdersSystemComponent;
    [SerializeField] private Button buildButton;
    [SerializeField] private Button orderButton;

    private OrderSystemsSwitcher switcher = new OrderSystemsSwitcher();

    private void Awake()
    {
        var buildOrderSystem = new OrderSystem();
        var unitOrderSystemOS = new OrderSystem();

        buildSystem = (IBuildSystem)buildSystemComponent;
        unitOrderSystem = (IUnitOrdersSystem)unitOrdersSystemComponent;

        buildSystem.Init(buildOrderSystem);
        unitOrderSystem.Init(unitOrderSystemOS);

        buildButton.onClick.AddListener(() => switcher.SwitchSystem(buildOrderSystem));
        orderButton.onClick.AddListener(() => switcher.SwitchSystem(unitOrderSystemOS));
    }
}



public interface IUnitOrdersSystem {
    void Init(OrderSystem sys);
}