using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : MonoBehaviour, IPool<GameObject>
{
    public static FireBallPool Instance { get; private set; }

    public int poolSize = 100;
    public GameObject arrowPrefab;

    private Queue<GameObject> arrowPool;

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
        arrowPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject asteroid = Instantiate(arrowPrefab);
            asteroid.SetActive(false); // Initially inactive
            arrowPool.Enqueue(asteroid);
        }
    }

    // Get an asteroid from the pool
    public GameObject GetObject()
    {
        if (arrowPool.Count > 0)
        {
            GameObject obj = arrowPool.Dequeue();
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
        if (arrowPool.Contains(obj))
        {
            Debug.LogWarning("Object already in the pool.");
            return;
        }
        obj.SetActive(false);
        arrowPool.Enqueue(obj);
    }

    // Clear the pool (if needed)
    public void ClearPool()
    {
        while (arrowPool.Count > 0)
        {
            GameObject asteroid = arrowPool.Dequeue();
            Destroy(asteroid);
        }
    }
}
