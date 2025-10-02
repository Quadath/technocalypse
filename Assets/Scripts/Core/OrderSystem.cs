public class OrderSystem
{
    public bool IsActive { get; private set; } = false;
    public event System.Action<bool> OnStateChanged;
    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
        OnStateChanged?.Invoke(IsActive);
    }
    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        OnStateChanged?.Invoke(IsActive);
    }
}
