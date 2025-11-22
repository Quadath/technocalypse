using System;
using UnityEngine;

namespace Game.Shared.Core
{ 
    public interface ITargetable
    {
        Transform Transform { get; }
        void TakeDamage(int amount);
        public void AddOnDeathListener(Action<IUnit> listener);
        public void RemoveOnDeathListener(Action<IUnit> listener);
    }
}
