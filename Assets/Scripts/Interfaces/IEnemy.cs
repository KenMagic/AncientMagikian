using UnityEngine;
public interface IEnemy : IDamagable
{
    void Initialize(Transform spawnPoint);
    void OnDeath();
}
