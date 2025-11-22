using UnityEngine;
using System.Collections.Generic;
using Game.Shared.Core;

namespace Game.Shared.SO
{
    public interface IUnitData
    {
        public string UnitName { get; }
        public GameObject Prefab { get; }
        public float Speed { get; }
        public int HitPoints { get; }
        public List<IUnitBehaviourData> Behaviours { get; }
    }
}
