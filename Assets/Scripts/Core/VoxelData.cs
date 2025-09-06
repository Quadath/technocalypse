using UnityEngine;
using System.Collections.Generic;

public static class VoxelData
{
    public static List<VoxelPattern> StraigtSlopePatterns = new List<VoxelPattern>();
    public static List<VoxelPattern> CornerSlopePatterns = new List<VoxelPattern>();
    public static List<VoxelPattern> WallsPatterns = new List<VoxelPattern>();


    public static int NormalSlopeCost = 25;
    public static int CornerSlopeCost = 1000000;

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

    public static readonly Vector3Int[] Neighbours = new Vector3Int[26] {
        new Vector3Int(-1, -1, -1), //0
        new Vector3Int(0, -1, -1), //1
        new Vector3Int(1, -1, -1), //2 
        new Vector3Int(-1, -1, 0), //3
        new Vector3Int(0, -1, 0), //4
        new Vector3Int(1, -1, 0), //5
        new Vector3Int(-1, -1, 1), //6
        new Vector3Int(0, -1, 1), //7
        new Vector3Int(1, -1, 1), //8
        new Vector3Int(-1, 0, -1), //9
        new Vector3Int(0, 0, -1), //10
        new Vector3Int(1, 0, -1), //11
        new Vector3Int(-1, 0, 0), //12
        new Vector3Int(1, 0, 0), //13
        new Vector3Int(-1, 0, 1), //14
        new Vector3Int(0, 0, 1), //15
        new Vector3Int(1, 0, 1), //16
        new Vector3Int(-1, 1, -1), //17
        new Vector3Int(0, 1, -1), //18
        new Vector3Int(1, 1, -1), //19
        new Vector3Int(-1, 1, 0), //20
        new Vector3Int(0, 1, 0), //21
        new Vector3Int(1, 1, 0), //22
        new Vector3Int(-1, 1, 1), //23
        new Vector3Int(0, 1, 1), //24
        new Vector3Int(1, 1, 1), //25
    };

    static VoxelData()
    {
        //z+
        StraigtSlopePatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 4, 15, 3, 5 },
            ForbiddenNeighbors = new int[] { },
            Vertices = new int[] { 1, 0, 7, 6 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = true,
            TraversalCost = NormalSlopeCost
        });
        //x+
        StraigtSlopePatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 4, 13, 1, 7 },
            ForbiddenNeighbors = new int[] { },
            Vertices = new int[] { 0, 4, 6, 2 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = true,
            TraversalCost = NormalSlopeCost
        });
        //z-
        StraigtSlopePatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 4, 10, 3, 5 },
            ForbiddenNeighbors = new int[] { },
            Vertices = new int[] { 4, 5, 2, 3 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = true,
            TraversalCost = NormalSlopeCost
        });
        //x-
        StraigtSlopePatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 4, 12, 1, 7 },
            ForbiddenNeighbors = new int[] { },
            Vertices = new int[] { 5, 1, 3, 7 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = true,
            TraversalCost = NormalSlopeCost
        });


        //Corner


        CornerSlopePatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 4, 16, 5, 7 },
            ForbiddenNeighbors = new int[] { 3 },
            Vertices = new int[] { 4, 6, 1 },
            Triangles = new int[] { 0, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(0,1),
            },
            IsSlope = true,
            TraversalCost = CornerSlopeCost
        });

        CornerSlopePatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 4, 14 },
            ForbiddenNeighbors = new int[] { 5 },
            Vertices = new int[] { 0, 7, 5 },
            Triangles = new int[] { 0, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(0,1),
            },
            IsSlope = true,
            TraversalCost = CornerSlopeCost
        });




        //Walls
        WallsPatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 15 },
            ForbiddenNeighbors = new int[] { },
            Vertices = new int[] { 5, 4, 7, 6 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = false,
            TraversalCost = NormalSlopeCost
        });
        WallsPatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 12 },
            ForbiddenNeighbors = new int[] { 4 },
            Vertices = new int[] { 4, 0, 3, 7 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = false,
            TraversalCost = NormalSlopeCost
        });
        WallsPatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 4 },
            ForbiddenNeighbors = new int[] { },
            Vertices = new int[] { 0, 4, 5, 1 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = false,
            TraversalCost = NormalSlopeCost
        });
        WallsPatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 13 },
            ForbiddenNeighbors = new int[] { 4 },
            Vertices = new int[] { 1, 5, 6, 2 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = false,
            TraversalCost = NormalSlopeCost
        });
        WallsPatterns.Add(new VoxelPattern
        {
            RequiredNeighbors = new int[] { 10 },
            ForbiddenNeighbors = new int[] { 4 },
            Vertices = new int[] { 0, 1, 2, 3 },
            Triangles = new int[] { 0, 1, 3, 3, 1, 2 },
            UVs = new Vector2[] {
                new Vector2(0,0), new Vector2(1,0),
                new Vector2(1,1), new Vector2(0,1)
            },
            IsSlope = false,
            TraversalCost = NormalSlopeCost
        });
    }
}
// {
//     public static readonly Vector3[] Verts = new Vector3[9] {
//         new Vector3(0, 0, 0), // 0
//         new Vector3(1, 0, 0), // 1
//         new Vector3(1, 1, 0), // 2
//         new Vector3(0, 1, 0), // 3
//         new Vector3(0, 0, 1), // 4
//         new Vector3(1, 0, 1), // 5
//         new Vector3(1, 1, 1), // 6
//         new Vector3(0, 1, 1),  // 7
//         new Vector3(0.5f, 0, 0.5f) // 8
//     };

//     public static readonly int[,] Tris = new int[6, 4] {
//         {0, 3, 1, 2}, // Back
//         {1, 2, 5, 6}, // Right
//         {0, 4, 3, 7}, // Left
//         {5, 6, 4, 7}, // Front
//         {3, 7, 2, 6}, // Top
//         {1, 5, 0, 4} // Bottom
//     };
//     public static readonly int[,] SlopeTris = new int[10, 4] {
//         {5, 4, 2, 3}, // Back
//         {4, 0, 6, 2}, // Right
//         {1, 5, 3, 7}, // Left
//         {0, 1, 7, 6}, // Front
//         {0, 0, 0, 0},
//         {0, 0, 0, 0},
//         {4, 8, 6, 1}, //x+z+
//         {0, 8, 7, 5}, //x-z+
//         {0, 2, 8, 5}, //x+z-
//         {1, 8, 3, 4} //x-z-
//     };

//     public static readonly Vector3Int[] FaceChecks = new Vector3Int[10] {
//         new Vector3Int(0, 0, -1), // back
//         new Vector3Int(1, 0, 0),  // right
//         new Vector3Int(-1, 0, 0), // left
//         new Vector3Int(0, 0, 1),  // front
//         new Vector3Int(0, 1, 0),  // top 
//         new Vector3Int(0, -1, 0), // bottom
//         new Vector3Int(1, 0, 1), //x+z+
//         new Vector3Int(-1, 0, 1), //x-z+
//         new Vector3Int(1, 0, -1), //x+z-
//         new Vector3Int(-1, 0, -1), //x-z-
//     };

//     public static readonly Vector2[] UVs = new Vector2[4] {
//         new Vector2(0, 0),
//         new Vector2(1, 0),
//         new Vector2(0, 1),
//         new Vector2(1, 1)
//     };
// }