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
            DebugDraw.DrawCube(target, 1f, new Color(0, 1f, 0, 0.5f));
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
}