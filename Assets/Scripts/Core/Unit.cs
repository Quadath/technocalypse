using System;
using UnityEngine;
using System.Collections.Generic;
using Game.Core.Behaviours;
using Game.Shared.Core;

namespace Game.Core
{
    public class Unit: IUnit
    {
        public int Player { get; }
        public float DetectionRange { get; } = 20;
        private readonly List<IUnitBehaviour> _behaviours = new();

        private event Action<Unit> OnDeath;
        private int _maxHitPoints;
        private int _hitPoints;

        public string DisplayedName { get; }
        public float Speed { get; }
        public Transform Transform { get; }
        public Rigidbody Rigidbody { get; private set; }
        public Vector3 NextPathPointPosition { get; set; }
        public Vector3 TargetDirection { get; set; }
        public Vector3 GoalPosition { get; set; }
        public bool HasGoal => GoalPosition != Vector3.zero;
        public PathfindingGrid PathfindingGrid;
        public bool IsAlive => _hitPoints > 0;
        public string Message { get; private set; }
        public event Action<string, string> OnMessage;

        public Action<IUnit> callback { get; set; }

        ~Unit()
        {
            callback(null);
        }

        public Unit(string displayedName, int player, Transform transform, Rigidbody rigidbody, float speed, int hp)
        {
            DisplayedName = displayedName;
            Player = player;
            Transform = transform;
            Rigidbody = rigidbody;
            Speed = speed;
            _hitPoints = hp;
            _maxHitPoints = hp;
        }

        public void Tick(float deltaTime)
        {
            foreach (var b in _behaviours)
                b.OnTick(deltaTime);
        }

        public void MoveTo(Vector3Int g)
        {
            //I have to move it to MoveBehaviour
            var mb = GetBehaviour<MoveBehaviour>();
            var start = Vector3Int.RoundToInt(Transform.position);
            var finder = new AStar3D();
            var gridPath = finder.FindPath(start, g, pos => PathfindingGrid.IsWalkable(pos));
            if (mb != null) mb.SetPath(gridPath);
        }

        public void AddBehaviour(IUnitBehaviour b) => _behaviours.Add(b);
        public void AddOnDeathListener(Action<IUnit> listener) => OnDeath += listener;
        public void RemoveOnDeathListener(Action<IUnit> listener) => OnDeath -= listener;

        public T GetBehaviour<T>() where T : class, IUnitBehaviour
        {
            foreach (var b in _behaviours)
                if (b is T t)
                    return t;
            return null;
        }

        public void TakeDamage(int amount)
        {
            _hitPoints -= amount;
            if (_hitPoints <= 0)
            {
                Die();
                return;
            }

            DebugMessage("Unit", $"got damage. HP is {_hitPoints}");
        }

        private void Die()
        {
            OnDeath?.Invoke(this);
        }

        public void DebugMessage(string source, string msg)
        {
            Message = msg;
            OnMessage?.Invoke(source, Message);
        }
    }
}