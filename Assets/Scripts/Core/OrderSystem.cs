public class OrderSystem
{
    public bool Active { get; private set; } = false;

    public void Activate()
    {
        Active = true;
    }
    public void Deactivate()
    {
        Active = false;
    }
}
