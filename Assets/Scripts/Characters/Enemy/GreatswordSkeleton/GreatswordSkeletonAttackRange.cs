using UnityEngine;

public class GreatswordSkeletonAttackRange : MonoBehaviour
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
        if (collision.CompareTag("Player"))
        {
            greatswordSkeleton.SetAttackState();
        }
        if (collision.CompareTag("Tower"))
        {
            greatswordSkeleton.SetAttackState();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            greatswordSkeleton.SetAttackState();
        }
        if (collision.CompareTag("Tower"))
        {
            greatswordSkeleton.SetAttackState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            greatswordSkeleton.SetMoveState();
        }
    }
}
