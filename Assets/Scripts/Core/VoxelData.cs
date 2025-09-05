using UnityEngine;

public static class VoxelData
{
    public static readonly Vector3[] Verts = new Vector3[9] {
        new Vector3(0, 0, 0), // 0
        new Vector3(1, 0, 0), // 1
        new Vector3(1, 1, 0), // 2
        new Vector3(0, 1, 0), // 3
        new Vector3(0, 0, 1), // 4
        new Vector3(1, 0, 1), // 5
        new Vector3(1, 1, 1), // 6
        new Vector3(0, 1, 1),  // 7
        new Vector3(0.5f, 0, 0.5f) // 8
    };

    public static readonly int[,] Tris = new int[6, 4] {
        {0, 3, 1, 2}, // Back
        {1, 2, 5, 6}, // Right
        {0, 4, 3, 7}, // Left
        {5, 6, 4, 7}, // Front
        {3, 7, 2, 6}, // Top
        {1, 5, 0, 4} // Bottom
    };
    public static readonly int[,] SlopeTris = new int[10, 4] {
        {5, 4, 2, 3}, // Back
        {4, 0, 6, 2}, // Right
        {1, 5, 3, 7}, // Left
        {0, 1, 7, 6}, // Front
        {0, 0, 0, 0},
        {0, 0, 0, 0},
        {4, 8, 6, 1}, //x+z+
        {0, 8, 7, 5}, //x-z+
        {0, 2, 8, 5}, //x+z-
        {1, 8, 3, 4} //x-z-
    };

    public static readonly Vector3Int[] FaceChecks = new Vector3Int[10] {
        new Vector3Int(0, 0, -1), // back
        new Vector3Int(1, 0, 0),  // right
        new Vector3Int(-1, 0, 0), // left
        new Vector3Int(0, 0, 1),  // front
        new Vector3Int(0, 1, 0),  // top 
        new Vector3Int(0, -1, 0), // bottom
        new Vector3Int(1, 0, 1), //x+z+
        new Vector3Int(-1, 0, 1), //x-z+
        new Vector3Int(1, 0, -1), //x+z-
        new Vector3Int(-1, 0, -1), //x-z-
    };

    public static readonly Vector2[] UVs = new Vector2[4] {
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(0, 1),
        new Vector2(1, 1)
    };
}