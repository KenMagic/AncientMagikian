using UnityEngine;

public class SkeletonArcherPOV : MonoBehaviour
{
    [SerializeField] private float delayToForgetTarget = 1.5f;

    private SkeletonArcher skeletonArcher;
    private Coroutine forgetTargetCoroutine;

    private void Awake()
    {
        skeletonArcher = GetComponentInParent<SkeletonArcher>();
        if (skeletonArcher == null)
        {
            Debug.LogError("SkeletonArcherPOV: No SkeletonArcher component found in parent.");
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skeletonArcher.SetNewTarget(collision.transform);
            skeletonArcher.SetAttackState();
        }

        if (collision.CompareTag("Tower"))
        {
            skeletonArcher.SetAttackState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skeletonArcher.SetMoveState();
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);

            skeletonArcher.StartForgetTargetCoroutine(delayToForgetTarget);
        }
    }
}
