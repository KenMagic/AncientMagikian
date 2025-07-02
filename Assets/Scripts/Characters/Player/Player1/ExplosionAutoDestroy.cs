using UnityEngine;

public class ExplosionAutoDestroy : MonoBehaviour
{
    public float lifetime = 0.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
