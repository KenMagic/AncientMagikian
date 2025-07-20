using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Orc,
    OrcArmoured,
    GreatswordSkeleton,
    SkeletonArcher,
    Slime,
    Werebear,
    Boss
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public List<Wave> waves;

    public Transform spawnPoint;

    private int currentWaveIndex = 0;
    private int aliveEnemies = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 

    public void StartWaves()
    {
        currentWaveIndex = 0;
        SpawnCurrentWave();
    }


    void SpawnCurrentWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("All waves completed!");
            GameController.Instance.GameWin = true;
            GameController.Instance.GameOver();
            return;
        }

        StartCoroutine(SpawnWaveCoroutine(waves[currentWaveIndex]));
    }

    IEnumerator SpawnWaveCoroutine(Wave wave)
    {
        foreach (var waveItem in wave.waveItems)
        {
            for (int i = 0; i < waveItem.count; i++)
            {
                SpawnFromPool(waveItem);
                yield return new WaitForSeconds(waveItem.spawnInterval);
            }
        }
    }


    void SpawnFromPool(WaveItem wave)
{
        EnemyType type = wave.type;
        GameObject enemy = null;
    switch (type)
    {
        case EnemyType.Orc:
            enemy = OrcPool.Instance.GetObject();
            break;

        case EnemyType.OrcArmoured:
            enemy = OrcArmoredPool.Instance.GetObject();
            break;
        case EnemyType.GreatswordSkeleton:
            enemy = GreatswordSkeletonPool.Instance.GetObject();
            break;
        case EnemyType.SkeletonArcher:
            enemy = SkeletonArcherPool.Instance.GetObject();
            break;
        case EnemyType.Slime:
            enemy = SlimePool.Instance.GetObject();
            break;
        case EnemyType.Werebear:
            enemy = WerebearPool.Instance.GetObject();
            break;
        case EnemyType.Boss:
            enemy = Instantiate(wave.enemyPrefab);
            break;
        default:
            break;
    }
        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);

            aliveEnemies++;
        }
}


    public void OnEnemyDied(int amount)
    {
        if (aliveEnemies >= 0)
        {
            GameSceneControl.Instance.AddExp(amount);
        }
        aliveEnemies--;
        if (aliveEnemies <= 0)
        {
            currentWaveIndex++;
            GameController.Instance.HighestWave = currentWaveIndex;
            SpawnCurrentWave();
        }
    }
}
