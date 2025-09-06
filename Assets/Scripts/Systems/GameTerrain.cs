// using UnityEngine;
// using System.Collections.Generic;

// public class GameTerrain : MonoBehaviour
// {
//     public int width = 16;
//     public int height = 16;
//     public int depth = 16;

//     private Block[,,] blocks;

//     private List<Vector3> vertices;
//     private List<int> triangles;
//     private List<Vector2> uvs;

//     private Mesh mesh;

//     void Start()
//     {
//         blocks = new Block[width, height, depth];
//         GenerateTerrain();
//         GenerateMesh();
//     }

//     void GenerateTerrain()
//     {
//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 for (int z = 0; z < depth; z++)
//                 {
//                     blocks[x, y, z] = new Block(Block.BlockType.Air);
//                 }
//             }
//         }

//         for (int x = 0; x < width; x++)
//         {
//             for (int z = 0; z < depth; z++)
//             {
//                 int groundHeight = 4; // flat ground
//                 for (int y = 0; y < groundHeight; y++)
//                 {
//                     if(y > 2) {
//                         if (x > 3 && x < 8) continue;
//                         if (z > 3 && z < 8) continue;
//                     }
//                     blocks[x, y, z] = new Block(Block.BlockType.Sand);
//                 }
//             }
//         }
//     }

//     void GenerateMesh()
//     {
//         vertices = new List<Vector3>();
//         triangles = new List<int>();
//         uvs = new List<Vector2>();

//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 for (int z = 0; z < depth; z++)
//                 {
//                     if (blocks[x, y, z].Type != Block.BlockType.Air)
//                         continue;
//                     // Check 10 neighbors if has block underneath, otherwise 6
//                     bool hasBlockUnderneath = (y <= 0) ||
//                              (blocks[x, y-1, z].Type != Block.BlockType.Air);
//                     int[] neighbours = new int[10] {0,0,0,0,0,0,0,0,0,0};
//                     bool sideSlope = false;

//                     for (int face = 0; face < 6 + (4 * (hasBlockUnderneath == true ? 1 : 0)); face++)
//                     {
//                         if (face > 3 && sideSlope)
//                             continue; //prevent corner slopes from spawning
//                         Vector3Int neighborOffset = VoxelData.FaceChecks[face];
//                         int nx = x + neighborOffset.x;
//                         int ny = y + neighborOffset.y;
//                         int nz = z + neighborOffset.z;

//                         bool neighborIsSolid =
//                             !(nx < 0 || nx >= width ||
//                              ny < 0 || ny >= height ||
//                              nz < 0 || nz >= depth) &&
//                              blocks[nx, ny, nz].Type != Block.BlockType.Air;  
//                         if (neighborIsSolid) {
//                             if (face < 4) 
//                                 sideSlope = true;
//                             AddFace(new Vector3Int(x, y, z), face, hasBlockUnderneath);
//                         }
//                     }
//                 }
//             }
//         }

//         mesh = new Mesh();
//         mesh.vertices = vertices.ToArray();
//         mesh.triangles = triangles.ToArray();
//         mesh.uv = uvs.ToArray();
//         mesh.RecalculateNormals();

//         MeshFilter mf = gameObject.AddComponent<MeshFilter>();
//         MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
//         MeshCollider mc = gameObject.GetComponent<MeshCollider>();

//         mf.mesh = mesh;
//         mc.sharedMesh = mesh;
//     }

//     void AddFace(Vector3Int pos, int face, bool canBeSlope)
//     {
//         int vCount = vertices.Count;

//         for (int i = 0; i < 4; i++)
//         {
//             if ((face < 4 || face > 5) && canBeSlope) {
//                 vertices.Add(pos + VoxelData.Verts[VoxelData.SlopeTris[face, i]]);
//             } else {
//                 vertices.Add(pos + VoxelData.Verts[VoxelData.Tris[face, i]]);
//             }
//             uvs.Add(VoxelData.UVs[i]);
//         }

//         triangles.Add(vCount + 0);
//         triangles.Add(vCount + 2);
//         triangles.Add(vCount + 1);
//         triangles.Add(vCount + 2);
//         triangles.Add(vCount + 3);
//         triangles.Add(vCount + 1);
//     }
// }