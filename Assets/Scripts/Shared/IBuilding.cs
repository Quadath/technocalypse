using System;
using Game.Shared.Core;
using UnityEngine;
public interface IBuilding: ITargetable
{
    public int Player { get; }
    public Vector3Int Origin { get; }
    public T GetBehaviour<T>() where T : class, IBuildingBehaviour;
}
