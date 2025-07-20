using UnityEngine;

[System.Serializable]
public class WaveItem
{
    public GameObject enemyPrefab;
    public EnemyType type;
    public int count;
    public float spawnInterval = 1f;
}
