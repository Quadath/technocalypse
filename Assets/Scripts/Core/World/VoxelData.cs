using UnityEngine;

public static class VoxelData
{
    public static readonly Vector3[] Verts = new Vector3[8] {
        new Vector3(0, 0, 0), // 0
        new Vector3(1, 0, 0), // 1
        new Vector3(1, 1, 0), // 2
        new Vector3(0, 1, 0), // 3
        new Vector3(0, 0, 1), // 4
        new Vector3(1, 0, 1), // 5
        new Vector3(1, 1, 1), // 6
        new Vector3(0, 1, 1)  // 7
    };

    public static readonly int[,] Tris = new int[6, 4] {
        {1, 0, 2, 3}, // Back
        {5, 1, 6, 2}, // Right
        {0, 4, 3, 7}, // Left
        {3, 7, 2, 6}, // Top
        {1, 5, 0, 4}, // Bottom
        {4, 5, 7, 6}  // Front
    };

    public static readonly Vector3Int[] FaceChecks = new Vector3Int[6] {
        new Vector3Int(0, 0, -1), // back
        new Vector3Int(1, 0, 0),  // right
        new Vector3Int(-1, 0, 0), // left
        new Vector3Int(0, 1, 0),  // top
        new Vector3Int(0, -1, 0), // bottom
        new Vector3Int(0, 0, 1)   // front
    };
    private static float texel = 1f / 64f;
    public static readonly Vector2[] UVs = new Vector2[4] {
        new Vector2(0 + texel, 0.75f + texel),
        new Vector2(0.25f - texel, 0.75f + texel),
        new Vector2(0 + texel, 1 - texel),
        new Vector2(0.25f - texel, 1 - texel)
    };
}