using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour, IPool<GameObject>
{
    public static SlimePool Instance { get; private set; }

    public int poolSize = 100;
    public GameObject skeletonArcherPrefab;

    private Queue<GameObject> skeletonArcherPool;

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
        skeletonArcherPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject asteroid = Instantiate(skeletonArcherPrefab);
            asteroid.SetActive(false); // Initially inactive
            skeletonArcherPool.Enqueue(asteroid);
        }
    }

    // Get an asteroid from the pool
    public GameObject GetObject()
    {
        if (skeletonArcherPool.Count > 0)
        {
            GameObject obj = skeletonArcherPool.Dequeue();
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
        if (skeletonArcherPool.Contains(obj))
        {
            Debug.LogWarning("Object already in the pool.");
            return;
        }
        obj.GetComponent<Slime>().ResetStatus();
        obj.SetActive(false);
        skeletonArcherPool.Enqueue(obj);
    }

    // Clear the pool (if needed)
    public void ClearPool()
    {
        while (skeletonArcherPool.Count > 0)
        {
            GameObject asteroid = skeletonArcherPool.Dequeue();
            Destroy(asteroid);
        }
    }
}
