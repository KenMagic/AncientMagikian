using UnityEngine;

public class GreatswordSkeletonPOV : MonoBehaviour
{
    [SerializeField] private float delayToForgetTarget = 1f;

    private GreatswordSkeleton greatswordSkeleton;
    private Coroutine forgetTargetCoroutine;

    void Awake()
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
            greatswordSkeleton.SetNewTarget(collision.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            greatswordSkeleton.SetNewTarget(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            greatswordSkeleton.StartForgetTargetCoroutine(delayToForgetTarget);
        }
    }
}
