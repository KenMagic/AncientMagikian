using UnityEngine;
public interface IPoolable
{
    void Initialize(Vector3 position);
    void OnSpawn();
    void ReturnToPool();
}