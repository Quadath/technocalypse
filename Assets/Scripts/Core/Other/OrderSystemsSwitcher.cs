public class OrderSystemsSwitcher
{
    private OrderSystem? currentSystem;

    public void SwitchSystem(OrderSystem sys)
    {
        if (currentSystem != null)
            currentSystem.Deactivate();
        currentSystem = sys;
        currentSystem.Activate();
    }
}
