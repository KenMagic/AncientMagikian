using UnityEngine;

public class SkeletonArcherHitbox : MonoBehaviour
{
    private SkeletonArcher skeletonArcher;

    private void Awake()
    {
        skeletonArcher = GetComponentInParent<SkeletonArcher>();
        if (skeletonArcher == null)
        {
            Debug.LogError("SkeletonArcherHitbox: No SkeletonArcher component found in parent.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skeletonArcher.SetAttackState();
        }
        if (collision.CompareTag("Tower"))
        {
            skeletonArcher.SetAttackState();
        }
        if (collision.CompareTag("PlayerAttack"))
        {
            skeletonArcher.SetHurtState();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack") || collision.CompareTag("Tower"))
        {
            skeletonArcher.SetMoveState();
        }
    }
}
