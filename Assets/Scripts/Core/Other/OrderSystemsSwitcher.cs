using JetBrains.Annotations;

public class OrderSystemsSwitcher
{
    [CanBeNull] private OrderSystem _currentSystem;

    public void SwitchSystem(OrderSystem sys)
    {
        _currentSystem?.Deactivate();
        _currentSystem = sys;
        _currentSystem?.Activate();
    }
}
