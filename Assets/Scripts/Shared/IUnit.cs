using UnityEngine;
using System;

namespace Game.Shared.Core
{
    public interface IUnit: ITargetable
    { 
        int Player { get; }
        float DetectionRange { get; }
        float Speed { get; }
        public Vector3 NextPathPointPosition { get; set; }
        public Vector3 TargetDirection { get; set; }
        void MoveTo(Vector3Int g);
        T GetBehaviour<T>() where T : class, IUnitBehaviour;
        void Tick(float deltaTime);
        void AddBehaviour(IUnitBehaviour b);
        Action<IUnit> callback { set; }
    }
}