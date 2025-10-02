using UnityEngine;

public class TeamPainter : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] renderers;
    [SerializeField] private PaintData data;

    public void Repaint(int player)
    {
        if (player > data.playerColors.Length) return;
        foreach (var r in renderers)
        {
            r.sharedMaterial = data.playerColors[player];
        }
    }
}
