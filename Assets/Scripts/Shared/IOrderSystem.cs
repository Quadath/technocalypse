using System.Collections.Generic;
public interface IOrderSystem
{
    bool IsActive { get; }
    event System.Action<bool> OnStateChanged;
}
