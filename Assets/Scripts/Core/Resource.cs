public class Resource
{
    public string Name { get; }
    public int Amount { get; private set; }

    public Resource(string name, int amount = 0)
    {
        Name = name;
        Amount = amount;
    }

    public void Add(int value) => Amount += value;
    public bool TrySpend(int value)
    {
        if (Amount < value) return false;
        Amount -= value;
        return true;
    }
}