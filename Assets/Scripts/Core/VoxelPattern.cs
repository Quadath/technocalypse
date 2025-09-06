using UnityEngine;
using System.Collections.Generic;

public class VoxelPattern
{
    public int[] RequiredNeighbors;
    public int[] ForbiddenNeighbors;
    public int[] Vertices;
    public int[] Triangles;
    public Vector2[] UVs;
    public bool IsSlope;
    public int TraversalCost;
}
