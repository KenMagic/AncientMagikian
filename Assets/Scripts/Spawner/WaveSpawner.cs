using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveData waveData;

    private void Start()
    {
        if (waveData == null)
        {
            Debug.LogError("WaveData chưa được gán!");
            return;
        }

        foreach (var entry in waveData.enemiesInWave)
        {
            StartCoroutine(SpawnEnemiesFromPool(entry));
        }
    }

    IEnumerator SpawnEnemiesFromPool(WaveEnemyEntry entry)
    {
        Component pool = GetPoolInstance(entry.poolName);
        if (pool == null)
        {
            Debug.LogError($"Không tìm thấy Pool: {entry.poolName}");
            yield break;
        }

        var getMethod = pool.GetType().GetMethod("GetObject");
        if (getMethod == null)
        {
            Debug.LogError($"Pool {entry.poolName} không có hàm GetObject()");
            yield break;
        }

        if (entry.startDelay > 0f)
            yield return new WaitForSeconds(entry.startDelay);

        for (int i = 0; i < entry.amountToSpawn; i++)
        {
            GameObject enemy = getMethod.Invoke(pool, null) as GameObject;
            if (enemy != null)
            {
                enemy.transform.position = transform.position;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);
            }

            yield return new WaitForSeconds(entry.spawnInterval);
        }
    }

    private Component GetPoolInstance(string poolName)
    {
        var type = System.Type.GetType(poolName);
        if (type == null)
        {
            Debug.LogError($"Không tìm thấy kiểu {poolName}");
            return null;
        }

        var instanceProp = type.GetProperty("Instance");
        if (instanceProp == null)
        {
            Debug.LogError($"{poolName} không có property Instance");
            return null;
        }

        return instanceProp.GetValue(null) as Component;
    }
}
