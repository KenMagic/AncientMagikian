using System.Collections.Generic;
using UnityEngine;

public class GreatswordSkeletonPool : MonoBehaviour, IPool<GameObject>
{
    public static GreatswordSkeletonPool Instance { get; private set; }

    public int poolSize = 100;
    public GameObject asteroidPrefab;

    private Queue<GameObject> asteroidPool;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }
    // Initialize the asteroid pool
    void InitializePool()
    {
        asteroidPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefab);
            asteroid.SetActive(false); // Initially inactive
            asteroidPool.Enqueue(asteroid);
        }
    }

    // Get an asteroid from the pool
    public GameObject GetObject()
    {
        if (asteroidPool.Count > 0)
        {
            GameObject obj = asteroidPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("No available asteroid in the pool. Consider increasing the pool size.");
            return null;
        }
    }

    // Return an asteroid to the pool
    public void ReturnObject(GameObject obj)
    {
        //if object is in the pool, do nothing
        if (asteroidPool.Contains(obj))
        {
            Debug.LogWarning("Object already in the pool.");
            return;
        }
        obj.GetComponent<GreatswordSkeleton>().ResetStatus(); // Reset state
        obj.SetActive(false);
        asteroidPool.Enqueue(obj);
        WaveManager.Instance.OnEnemyDied(obj.GetComponent<GreatswordSkeleton>().enemyData.exp);
    }

    // Clear the pool (if needed)
    public void ClearPool()
    {
        while (asteroidPool.Count > 0)
        {
            GameObject asteroid = asteroidPool.Dequeue();
            Destroy(asteroid);
        }
    }
}
