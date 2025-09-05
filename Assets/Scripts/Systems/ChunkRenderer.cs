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

    void AddAllComponents() {
        mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.AddComponent<MeshRenderer>();
        mc = gameObject.AddComponent<MeshCollider>();
    }ц

    public void Rrender(Block[,,] blocks) {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();

        width = blocks.GetLength(0);
        height = blocks.GetLength(1);
        depth = blocks.GetLength(2);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    Debug.Log("Block " + x + " " + y + " " + z);
                    if (blocks[x, y, z].Type != Block.BlockType.Air)
                        continue;
                    // Check 10 neighbors if has block underneath, otherwise 6
                    bool hasBlockUnderneath = (y <= 0) ||
                             (blocks[x, y-1, z].Type != Block.BlockType.Air);
                    int[] neighbours = new int[10] {0,0,0,0,0,0,0,0,0,0};
                    bool sideSlope = false;

                    for (int face = 0; face < 6 + (4 * (hasBlockUnderneath == true ? 1 : 0)); face++)
                    {
                        Debug.Log("Face " + face);
                        if (face > 3 && sideSlope)
                            continue; //prevent corner slopes from spawning
                        Vector3Int neighborOffset = VoxelData.FaceChecks[face];
                        int nx = x + neighborOffset.x;
                        int ny = y + neighborOffset.y;
                        int nz = z + neighborOffset.z;

                        bool neighborIsSolid =
                            !(nx < 0 || nx >= width ||
                             ny < 0 || ny >= height ||
                             nz < 0 || nz >= depth) &&
                             blocks[nx, ny, nz].Type != Block.BlockType.Air;  
                        if (neighborIsSolid) {
                            if (face < 4) 
                                sideSlope = true;
                            AddFace(new Vector3Int(x, y, z), face, hasBlockUnderneath);
                        }
                    }
                }
            }
        }
        if (mf == null) {
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

    private void AddFace(Vector3Int pos, int face, bool canBeSlope)
    {
        int vCount = vertices.Count;

        for (int i = 0; i < 4; i++)
        {
            if ((face < 4 || face > 5) && canBeSlope) {
                vertices.Add(pos + VoxelData.Verts[VoxelData.SlopeTris[face, i]]);
            } else {
                vertices.Add(pos + VoxelData.Verts[VoxelData.Tris[face, i]]);
            }
            uvs.Add(VoxelData.UVs[i]);
        }

        triangles.Add(vCount + 0);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 1);
        triangles.Add(vCount + 2);
        triangles.Add(vCount + 3);
        triangles.Add(vCount + 1);
    }
}
