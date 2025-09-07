using UnityEngine;
using System.Collections.Generic;

public class ChunkRenderer : MonoBehaviour, IChunkRenderer
{
    private int width;
    private int height;
    private int depth;

    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uvs;

    private Mesh _mesh;

    MeshFilter mf;
    MeshRenderer mr;
    MeshCollider mc;

    public void Render(World world, Vector3Int chunkNumber)
    {
        width = world.ChunkSizeX;
        height = world.ChunkSizeY;
        depth = world.ChunkSizeZ;
        vertices = new List<Vector3>();  // rough guess
        triangles = new List<int>();
        uvs = new List<Vector2>();

        // For each block, add visible faces only
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                for (int z = 0; z < depth; z++)
                {

                    Vector3Int pos = new Vector3Int(chunkNumber.x * width + x,
                            chunkNumber.y * height + y,
                            chunkNumber.z * depth + z);
                    if (world.GetBlock(pos.x, pos.y, pos.z).Type == Block.BlockType.Air) continue;

                    // Check 6 directions
                    for (int face = 0; face < 6; face++)
                    {
                        Vector3Int n = pos + VoxelData.FaceChecks[face];

                        bool neighborIsAir =
                            n.x < 0 || n.x >= world.Width ||
                            n.y < 0 || n.y >= world.Height ||
                            n.z < 0 || n.z >= world.Depth ||
                            world.GetBlock(n.x, n.y, n.z).Type == Block.BlockType.Air;

                        if (neighborIsAir)
                            AddFace(new Vector3Int(x, y, z), face);
                    }
                }

        if (_mesh == null) _mesh = new Mesh();
        else _mesh.Clear();

        _mesh.indexFormat = (vertices.Count > 65000 ? UnityEngine.Rendering.IndexFormat.UInt32
                                                   : UnityEngine.Rendering.IndexFormat.UInt16);

        _mesh.SetVertices(vertices);
        _mesh.SetTriangles(triangles, 0);
        _mesh.SetUVs(0, uvs);
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        if (mf == null)
        {
            mf = gameObject.GetComponent<MeshFilter>();
            mr = gameObject.GetComponent<MeshRenderer>();
            mc = gameObject.GetComponent<MeshCollider>();
        }

        mf.sharedMesh = _mesh;
        mc.sharedMesh = _mesh;
    }

    void AddFace(Vector3Int pos, int face)
    {
        int vCount = vertices.Count;

        for (int i = 0; i < 4; i++)
        {
            vertices.Add(pos + VoxelData.Verts[VoxelData.Tris[face, i]]);
            uvs.Add(VoxelData.UVs[i]);
        }

        triangles.Add(vCount + 0);
        triangles.Add(vCount + 1);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 1);
        triangles.Add(vCount + 3);
    }
}
