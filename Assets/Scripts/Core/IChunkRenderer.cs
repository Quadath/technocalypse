using UnityEngine;
public interface IChunkRenderer
{
    void Render(World world, Vector3Int position);
}