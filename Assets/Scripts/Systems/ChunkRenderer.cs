using UnityEngine;
using System.Collections.Generic;

public class ChunkRenderer : MonoBehaviour
{
    private int width;
    private int height;
    private int depth;

    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uvs;

    private Mesh mesh;

    MeshFilter mf;
    MeshRenderer mr;
    MeshCollider mc;

    void AddAllComponents()
    {
        mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.AddComponent<MeshRenderer>();
        mc = gameObject.AddComponent<MeshCollider>();
    }

    public void Render(World world, Vector3Int chunkNumber)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();

        width = world.ChunkSizeX;
        height = world.ChunkSizeY;
        depth = world.ChunkSizeZ;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3Int blockPos = new Vector3Int(chunkNumber.x * width + x,
                            chunkNumber.y * height + y,
                            chunkNumber.z * depth + z);
                    if (world.GetBlock(blockPos.x, blockPos.y, blockPos.z).Type != Block.BlockType.Air)
                        continue;
                    // Quaternion rotation = Quaternion.identity;

                    // Create the cube
                    // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    // cube.transform.SetPositionAndRotation(blockPos, rotation);
                    blockPos = new Vector3Int(chunkNumber.x * width + x,
                            chunkNumber.y * height + y,
                            chunkNumber.z * depth + z);
                    bool straightSlopeCreated = false;
                    foreach (var pattern in VoxelData.StraigtSlopePatterns)
                    {
                        if (MatchesPattern(blockPos, pattern, world))
                        {
                            AddPatternToMesh(pattern, new Vector3Int(x, y, z), vertices, triangles, uvs);
                        }
                    }
                    if (!straightSlopeCreated)
                    {
                        foreach (var pattern in VoxelData.CornerSlopePatterns)
                        {
                            if (MatchesPattern(blockPos, pattern, world))
                            {
                                AddPatternToMesh(pattern, new Vector3Int(x, y, z), vertices, triangles, uvs);
                            }
                        }
                    }
                    foreach (var pattern in VoxelData.WallsPatterns)
                    {
                        if (MatchesPattern(blockPos, pattern, world))
                        {
                            AddPatternToMesh(pattern, new Vector3Int(x, y, z), vertices, triangles, uvs);
                        }
                    }
                }
            }
        }
        if (mf == null)
        {
            AddAllComponents();
        }

        mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mf.mesh = mesh;
        mc.sharedMesh = mesh;
    }

    private void AddPatternToMesh(VoxelPattern pattern, Vector3Int blockPos,
        List<Vector3> meshVerts, List<int> meshTris, List<Vector2> meshUvs)
    {
        int startIndex = meshVerts.Count;
        foreach (var v in pattern.Vertices)
        {
            meshVerts.Add(blockPos + VoxelData.Verts[v]);
        }
        foreach (var t in pattern.Triangles)
        {
            meshTris.Add(startIndex + t);
        }
        foreach (var uv in pattern.UVs)
        {
            meshUvs.Add(uv);
        }
    }

    private bool MatchesPattern(Vector3Int pos, VoxelPattern pattern, World world)
    {
        foreach (var n in pattern.RequiredNeighbors)
        {
            Vector3Int offset = VoxelData.Neighbours[n];
            var nPos = pos + offset;
            if (!InBounds(nPos, world) || world.GetBlock(nPos.x, nPos.y, nPos.z).Type == Block.BlockType.Air)
                return false;
        }

        foreach (var n in pattern.ForbiddenNeighbors)
        {
            Vector3Int offset = VoxelData.Neighbours[n];
            var nPos = pos + offset;
            if (InBounds(nPos, world) && world.GetBlock(nPos.x, nPos.y, nPos.z).Type != Block.BlockType.Air)
                return false;
        }

        return true;
    }
    
    private bool InBounds(Vector3Int pos, World world)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.z >= 0 &&
            pos.x < world.Width &&
            pos.y < world.Height &&
            pos.z < world.Depth;
    }


    // private void AddFace(Vector3Int pos, int face, bool canBeSlope)
    // {
    //     int vCount = vertices.Count;

    //     for (int i = 0; i < 4; i++)
    //     {
    //         if ((face < 4 || face > 5) && canBeSlope)
    //         {
    //             vertices.Add(pos + VoxelData.Verts[VoxelData.SlopeTris[face, i]]);
    //         }
    //         else
    //         {
    //             vertices.Add(pos + VoxelData.Verts[VoxelData.Tris[face, i]]);
    //         }
    //         uvs.Add(VoxelData.UVs[i]);
    //     }

    //     triangles.Add(vCount + 0);
    //     triangles.Add(vCount + 2);
    //     triangles.Add(vCount + 1);
    //     triangles.Add(vCount + 2);
    //     triangles.Add(vCount + 3);
    //     triangles.Add(vCount + 1);
    // }
}
