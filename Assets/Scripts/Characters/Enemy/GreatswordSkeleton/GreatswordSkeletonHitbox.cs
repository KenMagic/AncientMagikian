using UnityEngine;

public class GreatswordSkeletonHitbox : MonoBehaviour
{
    private GreatswordSkeleton greatswordSkeleton;

    private void Awake()
    {
        greatswordSkeleton = GetComponentInParent<GreatswordSkeleton>();
        if (greatswordSkeleton == null)
        {
            Debug.LogError("GreatswordSkeleton component not found in parent object.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            greatswordSkeleton.SetHurtState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            greatswordSkeleton.SetAttackState();
        }
        greatswordSkeleton.SetMoveState();
    }
}
