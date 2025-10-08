using UnityEngine;
public interface IChunkRenderer
{
    void Render(IWorld world, Vector3Int position);
}