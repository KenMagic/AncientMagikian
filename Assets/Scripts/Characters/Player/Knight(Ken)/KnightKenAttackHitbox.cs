using System.Collections.Generic;
using UnityEngine;

public class KnightKenAttackHitbox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private KnightKen knightKen;
    public List<IDamagable> hitTargets = new List<IDamagable>();
    void Start()
    {
        knightKen = GetComponentInParent<KnightKen>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null && !hitTargets.Contains(damagable))
            {
                hitTargets.Add(damagable);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null && !hitTargets.Contains(damagable))
            {
                hitTargets.Add(damagable);
            }
        }
    }
}
