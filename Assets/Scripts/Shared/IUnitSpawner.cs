using Game.Shared.SO;
using UnityEngine;

namespace Game.Shared.Systems
{
    public interface IUnitSpawner
    {
        public void SpawnAt(Vector3 worldPos, Quaternion rot, IUnitData unitData, int player);
    }
}
