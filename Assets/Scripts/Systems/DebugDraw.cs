using UnityEngine;

public class DebugDraw
{
    public static void DrawCube(Vector3 center, float size, Color color, float duration = 0.016f)
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
        Debug.DrawLine(points[0], points[1], color, duration);
        Debug.DrawLine(points[1], points[2], color, duration);
        Debug.DrawLine(points[2], points[3], color, duration);
        Debug.DrawLine(points[3], points[0], color, duration);

        // Top edges
        Debug.DrawLine(points[4], points[5], color, duration);
        Debug.DrawLine(points[5], points[6], color, duration);
        Debug.DrawLine(points[6], points[7], color, duration);
        Debug.DrawLine(points[7], points[4], color, duration);

        // Vertical edges
        Debug.DrawLine(points[0], points[4], color, duration);
        Debug.DrawLine(points[1], points[5], color, duration);
        Debug.DrawLine(points[2], points[6], color, duration);
        Debug.DrawLine(points[3], points[7], color, duration);
    }
}
