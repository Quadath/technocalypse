using UnityEngine;
using System.Collections.Generic;
public class UnitView : MonoBehaviour
{
    public Unit Unit; // reference на core-unit
    private Rigidbody rb;
    private Queue<Vector3> path = new Queue<Vector3>();

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (path.Count == 0) return;

        Vector3 target = path.Peek();
        Vector3 dir = target - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude < 0.05f)
        {
            path.Dequeue();
            return;
        }

        Vector3 move = dir.normalized * Unit.Speed * Time.deltaTime;
        rb.MovePosition(transform.position + move);

        if (dir.sqrMagnitude > 0.001f)
        {
            DrawCube(target, 1f, new Color(0, 1f, 0, 0.5f));
            Quaternion targetRot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 5 * Time.deltaTime);
        }
    }

    public void SetPath(List<Vector3Int> gridPath, PathfindingGrid grid)
    {
        path.Clear();
        if (gridPath == null) return;

        foreach (var p in gridPath)
        {
            path.Enqueue(new Vector3(p.x + 0.5f, p.y, p.z + 0.5f));
        }
    }

    void DrawCube(Vector3 center, float size, Color color)
    {
        float half = size / 2f;
        Vector3[] points = new Vector3[8];
        points[0] = center + new Vector3(-half, -half, -half);
        points[1] = center + new Vector3(half, -half, -half);
        points[2] = center + new Vector3(half, -half, half);
        points[3] = center + new Vector3(-half, -half, half);
        points[4] = center + new Vector3(-half, half, -half);
        points[5] = center + new Vector3(half, half, -half);
        points[6] = center + new Vector3(half, half, half);
        points[7] = center + new Vector3(-half, half, half);

        // Bottom edges
        Debug.DrawLine(points[0], points[1], color);
        Debug.DrawLine(points[1], points[2], color);
        Debug.DrawLine(points[2], points[3], color);
        Debug.DrawLine(points[3], points[0], color);

        // Top edges
        Debug.DrawLine(points[4], points[5], color);
        Debug.DrawLine(points[5], points[6], color);
        Debug.DrawLine(points[6], points[7], color);
        Debug.DrawLine(points[7], points[4], color);

        // Vertical edges
        Debug.DrawLine(points[0], points[4], color);
        Debug.DrawLine(points[1], points[5], color);
        Debug.DrawLine(points[2], points[6], color);
        Debug.DrawLine(points[3], points[7], color);
    }
}